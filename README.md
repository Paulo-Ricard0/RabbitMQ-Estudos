<h3 align="center">
    <a href="https://www.rabbitmq.com/docs"><img alt="Banner RabbitMQ" title="RabbitMQ" src="https://taking.com.br/wp-content/uploads/2024/05/RabbitMQ-1.jpg" /></a>
</h3>

Nesse repositório contém meus estudos sobre RabbitMQ e exemplos de implementação utilizando diferentes algoritmos de roteamento de mensagens.

---

## 📋 Índice
- [Sobre RabbitMQ](#sobre-rabbitmq)
  - [O que é RabbitMQ](#o-que-é-rabbitmq)
  - [Como funciona RabbitMQ](#como-funciona-rabbitmq)
  - [Casos de Uso](#casos-de-uso)
- [Exchanges](#exchanges)
  - [Round Robin](#padrão-round-robin)
  - [Fanout](#fanout)
  - [Direct](#direct)
  - [Topic](#topic)
- [Request Reply](#request-reply)

---

## Sobre RabbitMQ

### O que é RabbitMQ
RabbitMQ é um agente de mensagens open-source amplamente utilizado que implementa o protocolo Advanced Message Queuing Protocol (AMQP). Ele é utilizado para enviar, receber e armazenar mensagens entre sistemas distribuídos, proporcionando uma forma eficiente e confiável de comunicação assíncrona entre diferentes componentes de software.

### Como funciona RabbitMQ
RabbitMQ funciona como um intermediário para mensagens, onde produtores enviam mensagens para exchanges. As exchanges roteiam as mensagens para filas com base em regras de roteamento. Consumidores então recebem as mensagens dessas filas. O RabbitMQ garante que as mensagens sejam entregues de forma confiável e permite a implementação de diferentes padrões de comunicação, como publish/subscribe, request/reply e round robin.

<img alt="#" title="RabbitMQ" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ.png?alt=media&token=826c8393-1adb-491d-8ab3-5fc53f56cb1a" />

### Casos de Uso
- **Processamento de Tarefas em Segundo Plano:** Utilizado para delegar tarefas de longa duração para serem processadas em segundo plano.
- **Comunicação entre Microserviços:** Facilita a comunicação assíncrona entre diferentes microserviços em uma arquitetura distribuída.
- **Filas de Mensagens:** Gerencia filas de mensagens em sistemas distribuídos, garantindo a entrega e o processamento ordenado.
- **Balanceamento de Carga:** Distribui tarefas uniformemente entre vários trabalhadores para otimizar o uso de recursos.

---

## Exchanges

### [Padrão (Round Robin)](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Round-Robin)

#### Explicação
No algoritmo Round Robin, as mensagens são distribuídas uniformemente entre os consumidores disponíveis. Cada mensagem é entregue ao próximo consumidor na fila, garantindo uma distribuição equilibrada de carga.

#### Como funciona
1. **Producer:** Envia mensagens para a exchange.
2. **Exchange:** Distribui as mensagens sequencialmente entre os consumidores, um de cada vez.
3. **Consumer:** Cada consumidor recebe mensagens na ordem em que são enviadas pela exchange.

<img alt="#" title="RabbitMQ-Round-Robin" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Round-Robin.png?alt=media&token=c20d54c6-7996-4496-951c-feca3045f295" />

#### Casos de Uso
- **Distribuição de Tarefas:** Ideal para balancear a carga de trabalho entre vários trabalhadores.
- **Processamento Paralelo:** Utilizado para dividir o processamento de grandes volumes de dados entre vários consumidores.

---

### [Fanout](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Fanout)

#### Explicação
A exchange do tipo Fanout envia todas as mensagens recebidas para todas as filas ligadas a ela, sem considerar qualquer chave de roteamento. Este padrão é útil para casos onde uma mensagem precisa ser entregue a múltiplos consumidores.

#### Como funciona
1. **Producer:** Envia mensagens para a exchange Fanout.
2. **Exchange:** A exchange Fanout retransmite cada mensagem para todas as filas que estão conectadas a ela.
3. **Consumer:** Todos os consumidores recebem todas as mensagens enviadas para a exchange.

<img alt="#" title="RabbitMQ-Fanout" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Fanout.png?alt=media&token=968e9e13-e6f1-4135-91d2-2e6681b76599" />

#### Casos de Uso
- **Broadcast de Mensagens:** Ideal para enviar notificações ou atualizações a todos os sistemas conectados.
- **Sistemas de Log:** Utilizado para enviar logs a diferentes sistemas de análise e armazenamento simultaneamente.

---

### [Direct](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Direct)

#### Explicação
A exchange do tipo Direct roteia mensagens para filas com base em uma chave de roteamento específica. Apenas as filas que estão vinculadas à exchange com a mesma chave de roteamento receberão a mensagem.

#### Como funciona
1. **Producer:** Envia mensagens para a exchange Direct com uma chave de roteamento específica.
2. **Exchange:** A exchange Direct encaminha as mensagens para as filas que possuem uma chave de roteamento correspondente.
3. **Consumer:** Os consumidores recebem mensagens apenas das filas cujas chaves de roteamento correspondem às chaves usadas ao enviar as mensagens.

<img alt="#" title="RabbitMQ-Direct" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Direct.png?alt=media&token=4b7ff07c-1e08-43c4-8185-9411b9a9bc0c" />

#### Casos de Uso
- **Roteamento de Mensagens:** Utilizado quando há necessidade de enviar mensagens a consumidores específicos com base em critérios definidos.
- **Filtragem de Logs:** Permite a separação de logs em diferentes níveis (erro, aviso, info) e envio a diferentes filas para processamento especializado.

---

### [Topic](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Topic)

### Explicação
A exchange do tipo Topic permite que as mensagens sejam roteadas para uma ou mais filas com base em padrões de chave de roteamento que utilizam curingas. Isso é útil para casos em que as mensagens precisam ser filtradas e enviadas para filas específicas com base em padrões de tópicos.

#### Como funciona
1. **Producer:** Envia mensagens para a exchange Topic com uma chave de roteamento contendo padrões.
2. **Exchange:** A exchange Topic usa curingas nas chaves de roteamento para determinar quais filas devem receber cada mensagem.
3. **Consumer:** Os consumidores recebem mensagens das filas que correspondem aos padrões definidos nas chaves de roteamento.

<img alt="#" title="RabbitMQ-Direct" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Topic.png?alt=media&token=72e604fc-44d8-48dc-80f7-2afc761a2dc1" />

#### Casos de Uso
- **Sistemas de Log:** Permite a entrega de logs para filas específicas com base em padrões, como níveis de severidade ou origens de log.
- **Roteamento Dinâmico:** Utilizado para enviar mensagens para diferentes filas com base em padrões de chave de roteamento.

---

## [Request-Reply](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Request-Reply)

### Explicação
O padrão Request-Reply no RabbitMQ é utilizado para permitir a comunicação síncrona entre um cliente (requester) e um servidor (replier). Esse padrão é comum em arquiteturas de microserviços e sistemas distribuídos onde um serviço precisa solicitar dados ou executar uma operação em outro serviço e aguardar uma resposta.

#### Como Funciona

1. **Produção de Mensagem (Request)**
    - O cliente (requester) envia uma mensagem de solicitação para uma fila ou exchange específica.
    - A mensagem inclui um identificador de correlação (correlation ID) e um endereço de resposta (reply-to), que é geralmente o nome de uma fila onde o cliente espera receber a resposta.

2. **Consumo da Solicitação**
    - O servidor (replier) consome a mensagem de solicitação da fila.
    - O servidor processa a solicitação e gera uma resposta.


3. **Produção de Mensagem (Reply)**
    - O servidor envia a mensagem de resposta de volta para a fila especificada no campo reply-to da mensagem original.
    - A resposta inclui o mesmo correlation ID para que o cliente possa associar a resposta com a solicitação correspondente.

4. **Consumo da Resposta**
    - O cliente consome a mensagem de resposta da fila de resposta.
    - O cliente usa o correlation ID para associar a resposta com a solicitação original e processar os dados recebidos.

<img alt="#" title="RabbitMQ-Request-Reply" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Request-Reply.png?alt=media&token=94fac98b-68d8-45b8-b0d3-3a9bc1789d64" />

#### Casos de Uso
- **Microserviços:** Comunicação síncrona entre microserviços, onde um serviço precisa solicitar dados ou operações de outro serviço.
- **Processamento de Solicitações:** Aplicações que exigem respostas imediatas para as solicitações enviadas, como consultas a bancos de dados ou serviços externos.
- **Integração de Sistemas:** Conectar sistemas legados onde é necessário enviar uma solicitação e esperar uma resposta para continuar o processamento.

<br>
<p align="right"><a href="#top"><img src="https://img.shields.io/static/v1?label&message=voltar+ao+topo&color=fb8200&style=flat&logo" alt="voltar ao topo" /></a></p>
