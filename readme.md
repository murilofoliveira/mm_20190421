
## Sobre o Desenvolvimento

Defini a meta de terminar esse projeto em 8h e usei as 8h para isso.

Assim posso ser avaliado em como produzo num curto espa�o de tempo para um projeto simples. 

Considerando que a vaga � backend meu frontend ficou totalmente sem valida��o.

O campo espera o formato definido (00:00:00,000) podendo ser tanto positivo como negativo.


## O projeto

Foquei o projeto em fazer o backend estruturado para que seja de f�cil manuten��o caso as tecnologias por tr�s sejam alteradas (BD por exemplo)
Pattern: DDD
Database: MongoDB


## Problemas encontrados
A ideia inicial era utilizar DynamoDB do AWS. Por�m encontrei problemas na utiliza��o dele j� que ele n�o converte StringBuilder ao salvar utilizando a DLL deles e tamb�m n�o seria performatico usar uma table apenas para o request.

A solu��o foi trocar para o MongoDB onde permitiria que eu salvasse toda a estrutura de uma s� vez mas que ainda conseguisse fazer um request eliminando o campo de legenda que � desnecess�rio para a index.

A parte de download do MemoryStream no .net Core tamb�m me deu um pouco de trabalho e solucionei clonando o MS antes de retornar para tela.

## Pontos a destacar

Para que nenhuma classe com exce��o de infra tenha conhecimento que o BD � o MongoDB fui obrigado a clonar minha entity e remover o Dictonary dela e transformar em List<string>.

Utilizei Dictionary na montagem do objecto pois entendo que a performance � melhor e me facilita em fazer o incremento do timestamp simultaneamente

MongoDB n�o foi a primeira op��o para evitar montar um ambiente, mas lembrei que poderia utilizar o MongoLab numa vers�o gratuita

No retorno da lista (index) eu coloquei no comando para nao vir o List<string> com as legendas para melhorar a performance. Esse campo s� vem na hora do download.

Problema de performance para carregar a Index tem rela��o com a base Free do MongoLab utilizada para esse teste

## Melhorias poss�veis

- Validar se o tempo removido fez com que o start time inicial ficasse negativo
- O sistema est� pronto para salvar tamb�m o SRT original, mas para isso terei de validar se o nome j� � existente
- Remover do c�digo o usuario/senha e passar para o .json
- Validar a integridade do arquivo srt enviado (se est� dentro dos padr�es)
- Validar a entrada do TimeStamp retornando erro amig�vel (no momento apenas estoura exception)
- Adicionar o IsValid() na classe SubtitleEntity. 
