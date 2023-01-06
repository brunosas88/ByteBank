# byte-bank
Projeto que simula o menu de operações de uma instituição bancária.

## Recursos Implementados

* Cadastrar usuário;
* Deletar usuário;
* Mostrar todos os usuários;
* Mostrar detalhes de um usuário;
* Mostrar o valor total armazenado no sistema;
* Realizar transações bancárias (depósito, saque, transferência, histórico de transações do usuário);
* Mostrar todas as transações feitas;
* Mostrar transações feitas a partir de um filtro de usuário ou data;
    

## Tecnologias Utilizadas
Aplicação de recursos básicos de C# como arrays para representação das características dos usuários cadastrados,
e utilização de laços de repetição e condição para confirmação de escolhas, tratamento de erros e apresentação de dados em tela utilizando o terminal.

## Atualizações

1. Mudança de estrutura de dados de array para uma lista do tipo Cliente, classe criada com os atributos básicos necessários para 
interação em um banco: nome, cpf, senha, número da conta e saldo; 
2. Criada operação de mostrar o balanço total armazenado no banco;
3. Criada operação de validar usuário cadastrado por cpf e senha e as operações para realização de transações bancárias de 
depósito, saque e transferêcia;
4. Criada validação de cpf ao ser registrado novo usuário para evitar duplicidade desse campo;
5. Criadas classes Display e Client para melhor estruturação do projeto e divisão de responsabilidades;
6. Adicionada persistência de dados sobre os clientes cadastrados;
7. Adicionada persistência de dados e geração de arquivo de texto com detalhes sobre transações realizadas (depósitos, saques, 
transferências); 
8. Adicionada validação de CPF e proteção para visualização da senha;
