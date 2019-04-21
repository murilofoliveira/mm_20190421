
## Sobre o Desenvolvimento

Defini a meta de terminar esse projeto em 8h e usei as 8h para isso.

Assim posso ser avaliado em como produzo num curto espaço de tempo para um projeto simples. 

Considerando que a vaga é backend meu frontend ficou totalmente sem validação.

O campo espera o formato definido (00:00:00,000) podendo ser tanto positivo como negativo.


## O projeto

Foquei o projeto em fazer o backend estruturado para que seja de fácil manutenção caso as tecnologias por trás sejam alteradas (BD por exemplo)
Pattern: DDD
Database: MongoDB


## Problemas encontrados
A ideia inicial era utilizar DynamoDB do AWS. Porém encontrei problemas na utilização dele já que ele não converte StringBuilder ao salvar utilizando a DLL deles e também não seria performatico usar uma table apenas para o request.

A solução foi trocar para o MongoDB onde permitiria que eu salvasse toda a estrutura de uma só vez mas que ainda conseguisse fazer um request eliminando o campo de legenda que é desnecessário para a index.

A parte de download do MemoryStream no .net Core também me deu um pouco de trabalho e solucionei clonando o MS antes de retornar para tela.

## Pontos a destacar

Para que nenhuma classe com exceção de infra tenha conhecimento que o BD é o MongoDB fui obrigado a clonar minha entity e remover o Dictonary dela e transformar em List<string>.

Utilizei Dictionary na montagem do objecto pois entendo que a performance é melhor e me facilita em fazer o incremento do timestamp simultaneamente

MongoDB não foi a primeira opção para evitar montar um ambiente, mas lembrei que poderia utilizar o MongoLab numa versão gratuita

No retorno da lista (index) eu coloquei no comando para nao vir o List<string> com as legendas para melhorar a performance. Esse campo só vem na hora do download.

Problema de performance para carregar a Index tem relação com a base Free do MongoLab utilizada para esse teste

## Melhorias possíveis

- Validar se o tempo removido fez com que o start time inicial ficasse negativo
- O sistema está pronto para salvar também o SRT original, mas para isso terei de validar se o nome já é existente
- Remover do código o usuario/senha e passar para o .json
- Validar a integridade do arquivo srt enviado (se está dentro dos padrões)
- Validar a entrada do TimeStamp retornando erro amigável (no momento apenas estoura exception)
- Adicionar o IsValid() na classe SubtitleEntity. 
