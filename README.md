# byte-bank
Projeto que simula o menu de operações de uma instituição bancária.

## Recursos Implementados
    1 - Cadastrar usuário;
    2 - Deletar usuário;
    3 - Mostrar todos os usuários;
    4 - Mostrar detalhes de um usuário;
    5 - Mostrar o valor total armazenado no sistema;
    6 - Realizar transações bancárias (depósito, saque, transferência);

## Tecnologias Utilizadas
Aplicação de recursos básicos de C# como arrays para representação das características dos usuários cadastrados,
e utilização de laços de repetição e condição para confirmação de escolhas, tratamento de erros e apresentação de dados em tela utilizando o terminal.

## Atualizações
    1 - Mudança de estrutura de dados de array para uma lista do tipo Cliente, classe criada com os atributos básicos necessários para 
    interação em um banco: nome, cpf, senha, número da conta e saldo; 
    2 - Criada operação de mostrar o balanço total armazenado no banco;
    3 - Criada operação de validar usuário cadastrado por cpf e senha e as operações para realização de transações bancárias de 
    depósito, saque e transferêcia;
    4 - Criada validação de cpf ao ser registrado novo usuário para evitar duplicidade desse campo;
    5 - Criadas classes Display e Client para divisão de responsabilidades;
