"""
Script para gerar relatórios de custos e análises.
"""

import json
from datetime import datetime, timedelta
from cost_explorer import CostExplorer
from resource_cleanup import ResourceCleanup


class ReportGenerator:
    def __init__(self, region='us-east-1'):
        """Inicializa o gerador de relatórios."""
        self.cost_explorer = CostExplorer(region=region)
        self.cleanup = ResourceCleanup(region=region)

    def generate_executive_summary(self, filename='executive_summary.json'):
        """
        Gera um resumo executivo dos custos.
        
        Args:
            filename (str): Nome do arquivo de saída
        """
        start_date = (datetime.now() - timedelta(days=30)).strftime('%Y-%m-%d')
        end_date = datetime.now().strftime('%Y-%m-%d')
        
        # Coletar dados
        service_costs = self.cost_explorer.get_service_costs(start_date, end_date)
        total_cost = sum(service_costs.values())
        
        # Encontrar serviços com maior custo
        top_services = sorted(service_costs.items(), key=lambda x: x[1], reverse=True)[:5]
        
        # Gerar resumo
        summary = {
            'periodo': {
                'inicio': start_date,
                'fim': end_date,
                'dias': 30
            },
            'custo_total': round(total_cost, 2),
            'custo_medio_diario': round(total_cost / 30, 2),
            'top_servicos': [
                {
                    'servico': service,
                    'custo': round(cost, 2),
                    'percentual': round((cost / total_cost * 100), 2)
                }
                for service, cost in top_services
            ],
            'recomendacoes': [
                rec for rec in self.cost_explorer.get_recommendations()
            ],
            'data_relatorio': datetime.now().isoformat()
        }
        
        with open(filename, 'w', encoding='utf-8') as f:
            json.dump(summary, f, indent=2, ensure_ascii=False)
        
        print(f"✓ Resumo executivo gerado: {filename}")
        return summary

    def generate_savings_opportunity_report(self, filename='savings_opportunities.txt'):
        """
        Gera um relatório de oportunidades de economia.
        
        Args:
            filename (str): Nome do arquivo de saída
        """
        with open(filename, 'w', encoding='utf-8') as f:
            f.write("=" * 70 + "\n")
            f.write("RELATÓRIO DE OPORTUNIDADES DE ECONOMIA\n")
            f.write(f"Gerado em: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n")
            f.write("=" * 70 + "\n\n")
            
            # Volumes não utilizados
            f.write("1. VOLUMES EBS NÃO UTILIZADOS\n")
            f.write("-" * 70 + "\n")
            volumes = self.cleanup.find_and_delete_unused_volumes(days=30, dry_run=True)
            if volumes.get('total_found', 0) > 0:
                f.write(f"   Volumes encontrados: {volumes['total_found']}\n")
                f.write(f"   Tamanho total: {volumes['total_gb']} GB\n")
                f.write(f"   Economia mensal estimada: ${volumes['estimated_savings']:.2f}\n")
            else:
                f.write("   Nenhum volume não utilizado encontrado.\n")
            
            f.write("\n2. ELASTIC IPs NÃO ASSOCIADOS\n")
            f.write("-" * 70 + "\n")
            eips = self.cleanup.find_unattached_elastic_ips(dry_run=True)
            if eips.get('total_found', 0) > 0:
                f.write(f"   IPs encontrados: {eips['total_found']}\n")
                f.write(f"   Economia mensal estimada: ${eips['estimated_savings']:.2f}\n")
            else:
                f.write("   Nenhum Elastic IP não associado encontrado.\n")
            
            f.write("\n3. SNAPSHOTS ANTIGOS\n")
            f.write("-" * 70 + "\n")
            snapshots = self.cleanup.find_old_snapshots(days=90, dry_run=True)
            if snapshots.get('total_found', 0) > 0:
                f.write(f"   Snapshots encontrados: {snapshots['total_found']}\n")
                f.write(f"   Tamanho total: {snapshots['total_gb']} GB\n")
                f.write(f"   Economia mensal estimada: ${snapshots['estimated_savings']:.2f}\n")
            else:
                f.write("   Nenhum snapshot antigo encontrado.\n")
            
            # Total
            total_savings = (
                volumes.get('estimated_savings', 0) +
                eips.get('estimated_savings', 0) +
                snapshots.get('estimated_savings', 0)
            )
            
            f.write("\n" + "=" * 70 + "\n")
            f.write(f"ECONOMIA TOTAL POTENCIAL: ${total_savings:.2f}/MÊS\n")
            f.write(f"ECONOMIA ANUAL POTENCIAL: ${total_savings * 12:.2f}\n")
            f.write("=" * 70 + "\n")
        
        print(f"✓ Relatório de oportunidades gerado: {filename}")

    def generate_dashboard_data(self, filename='dashboard_data.json'):
        """
        Gera dados para um dashboard.
        
        Args:
            filename (str): Nome do arquivo de saída
        """
        start_date = (datetime.now() - timedelta(days=30)).strftime('%Y-%m-%d')
        end_date = datetime.now().strftime('%Y-%m-%d')
        
        service_costs = self.cost_explorer.get_service_costs(start_date, end_date)
        total_cost = sum(service_costs.values())
        
        dashboard_data = {
            'metricas': {
                'custo_total_mes': round(total_cost, 2),
                'custo_dia_medio': round(total_cost / 30, 2),
                'numero_servicos': len(service_costs)
            },
            'servicos': [
                {
                    'nome': service,
                    'custo': round(cost, 2),
                    'percentual': round((cost / total_cost * 100), 2)
                }
                for service, cost in sorted(service_costs.items(), key=lambda x: x[1], reverse=True)
            ],
            'tendencias': {
                'semana_passada': round(self.cost_explorer.get_daily_costs(days=7).__len__() * (total_cost / 30), 2),
                'mes_passado': round(total_cost, 2)
            },
            'atualizado_em': datetime.now().isoformat()
        }
        
        with open(filename, 'w', encoding='utf-8') as f:
            json.dump(dashboard_data, f, indent=2, ensure_ascii=False)
        
        print(f"✓ Dados do dashboard gerados: {filename}")
        return dashboard_data


# Exemplo de uso
if __name__ == '__main__':
    generator = ReportGenerator(region='us-east-1')
    
    print("=== Gerador de Relatórios AWS ===\n")
    
    print("1. Gerando resumo executivo...")
    summary = generator.generate_executive_summary()
    
    print("\n2. Gerando relatório de oportunidades...")
    generator.generate_savings_opportunity_report()
    
    print("\n3. Gerando dados do dashboard...")
    generator.generate_dashboard_data()
    
    print("\n✓ Todos os relatórios foram gerados com sucesso!")
