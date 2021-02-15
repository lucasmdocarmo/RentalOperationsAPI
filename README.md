# Introduction 
Projeto desenho arquitetural de uma operação de Aluguel de Veiculos

# Getting Started

1.	Cadastrar Cliente
2.	Cadastrar Usuario ou Operador
3.	Autenticar - Gera Token JWT - Usar no Swagger para Autorizar chamadas subsequentes (Bearer <token>)
4.	Criar Marca, Modelo e Veiculo
5.  Criar Reserva ou Agendar Reserva
6.  Simular Reserva ou Usar Codigo de Reserva para gerar reserva.
7.  Gerar Contrato
8.  Devolver Contrato e Pagar Contrato
9.  Gerar PDF

# Build
Projeto possui geração de banco de dados automatico. Atualizar connection string para o seu SQLSERVER.
"AppOperacoesCatalog": "Server=localhost\\SEU_SQL_SERVER_AQUI;Database=LL_RentalOperacoes;Trusted_Connection=True;MultipleActiveResultSets=true"

#Arquitetura
1. Projeto segue arquitetura limpa usando CQRS e command handler para execução das ações(Commands) e Queries para consultas.
2. Fail Fast Validations
3. Notification Pattern
4. Domain Driven Development
5. Repository
6. Presentation Pattern
7. Docker
8. EF Core, Mappings
9. AutoMapper
10. OpenAPI
11. JWT Bearer Authentication
