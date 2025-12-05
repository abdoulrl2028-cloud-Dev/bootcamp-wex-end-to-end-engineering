# Projeto: Otimização de Custos AWS

Um projeto prático que demonstra como otimizar custos na AWS através de monitoramento, análise e automação.

## Objetivos

1. Monitorar gastos em tempo real
2. Identificar recursos subutilizados
3. Implementar políticas de economia
4. Automatizar a limpeza de recursos não utilizados

## Componentes

- **cost_explorer.py**: Análise de custos
- **resource_cleanup.py**: Remover recursos não utilizados
- **automation.py**: Automatizar economias
- **reports.py**: Gerar relatórios

## Funcionalidades

✓ Dashboard de custos
✓ Recomendações de otimização
✓ Detecção de recursos ociosos
✓ Automação de shutdown
✓ Alertas de custo

## Como Usar

```python
from cost_explorer import CostExplorer

explorer = CostExplorer()
costs = explorer.get_monthly_costs()
explorer.generate_report(costs)
```
