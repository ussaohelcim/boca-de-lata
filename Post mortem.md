# Introdução

Boca-de-lata foi um top-down-shooter de ação, desenvolvido por Michel Sousa, onde você iria controlar um cefalopode alinigena que foi exilado de seu planeta natal e acabara de aterrissar no planeta Terra.

Depois de chegar na Terra, esse alienigena foi recebido de maneira hostil pelos habitantes, dos quais tentavam matar o alienigena a todo custo.

O escopo deste projeto deveria ser pequeno o suficiente para caber numa deadline de 1 mês. Com lançamento previsto para dia 30 de julho.

# Objetivo do jogo

O objetivo do jogo era sobreviver e fugir do planeta terra. Para conseguir escapar o jogador deve sobreviver até o final da fase, onde ele encontraria um foguete prestes a ser mandado para o planeta Marte.

# Mecanicas

O jogo era controlado da maneira convencional em que se encontram os jogos de computador de top-down-shooter, movimentação pelo WASD e o mouse para mirar. 

O "diferencial" do Boca-de-lata estava em sua mecanica de absorver e atirar de volta os tiros que os humanos atiraram. 
O jogador só consegue absorver 30 balas por vez, caso ele tente absorver mais balas o jogador perde vida.

Essa mecanica era gerenciada por uma pilha FIFO (First in, first out), que eu chamei de estomago.

Os tiros teriam *status* diferentes, fazendo que o jogador use um pouco de estrategia na hora de decidir em quem ele vai atirar a bala que esta no topo da pilha do estomago, dentre os diferentes status tinha velocidade, tamanho, e eu iria implementar tambem danos diferentes.

Para recuperar a vida o jogador poderia "comer" as balas que estão no seu estomago, a cada 10 balas no estomago o jogador ganhava 1 ponto de vida.

No final da vida do jogo tambem foi desenvolvido a habilidade de "teletransportar" onde na verdade o personagem entraria de baixo da terra e aparecia na posição do mouse, com 5 novas pedras no estomago. Decidi fazer esta mecanica pois as batalhas 1x1 eram entediantes.

# Coisas implementadas

- Personagem principal
    - Movimentação
    - Atirar
    - Absorver balas
    - Entrar debaixo da terra
    - Receber dano
    - Comer
    - Indigestão

- Inimigos

    - Tipos

        Existe alguns tipos diferentes de personagem, dentre eles: caipira, policial, soldado, jipe, tanque de guerra, alienigena

    - Movimentação 
        
        A movimentação dos inimigos é feita via "inteligencia artificial". Cada 

    - Atirar

        As balas atiradas pelo inimigo são diferentes, de acordo ao tipo do inimigo

    - Receber dano

- Inteligencia artificial inimigos
    
    A "inteligencia artificial" foi feita via UnityEngine.AI com o NavMesh/NavMeshAgent. É uma maneira facil e rapida para fazer o inimigo se movimentar.

# Problemas

Até o momento, o unico problema foi o tamanho do escopo. Ao começar a fazer a arte comecei a "sentir" que a *deadline* não seria suficiente.

Demorei cerca de 2 semanas para desenvolver a base da programação do personagem, inimigos e balas. Esta base já estaria suficiente para testar todo o *game feel* do jogo.

Essa semana (5 de julho) eu comecei a desenvolver a arte, pensei em uma UI, comecei a fazer as texturas e comecei a modelar os personagens. No dia 7 de julho minha mente explodiu ao pensar em todo o trabalho que terei para fazer toda a parte de arte e implementar tudo aquilo no codigo, sendo assim me veio na mente que dia 30 de julho não seria suficiente para lançar o jogo e resolvi cancelar o jogo.

# Conclusão

Estou com 23 anos, ainda morando com meu pai, desempregado a 2 anos e em busca de mudar de vida. Deste modo decidi cancelar o lançamento do jogo para trabalhar em outro jogo ou em projetos de programação pro meu portifolio para buscar um emprego.
