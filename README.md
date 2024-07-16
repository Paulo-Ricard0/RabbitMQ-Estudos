<h3 align="center">
    <a href="https://www.rabbitmq.com/docs"><img alt="Banner RabbitMQ" title="RabbitMQ" src="https://taking.com.br/wp-content/uploads/2024/05/RabbitMQ-1.jpg" /></a>
</h3>

<h1 align="center">🐰 RABBITMQ</h1>

Nesse repositório contém meus estudos sobre RabbitMQ e exemplos de implementação utilizando diferentes algoritmos de roteamento de mensagens.

---

## 📋 Índice
- [Sobre RabbitMQ](#sobre-rabbitmq)
  - [O que é RabbitMQ](#o-que-é-rabbitmq)
  - [Como funciona RabbitMQ](#como-funciona-rabbitmq)
  - [Casos de Uso](#casos-de-uso)
- [Algoritmos](#algoritmos)
  - [Round Robin](#round-robin)
    - [Explicação](#explicação)
    - [Casos de Uso](#casos-de-uso-1)
  - [Fanout](#fanout)
    - [Explicação](#explicação-1)
    - [Casos de Uso](#casos-de-uso-2)
  - [Direct](#direct)
    - [Explicação](#explicação-2)
    - [Casos de Uso](#casos-de-uso-3)
   

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

## Algoritmos

### [Round Robin](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Round-Robin)

#### Explicação
No algoritmo Round Robin, as mensagens são distribuídas uniformemente entre os consumidores disponíveis. Cada mensagem é entregue ao próximo consumidor na fila, garantindo uma distribuição equilibrada de carga.

<img alt="#" title="RabbitMQ-Round-Robin" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Round-Robin.png?alt=media&token=c20d54c6-7996-4496-951c-feca3045f295" />

#### Casos de Uso
- **Distribuição de Tarefas:** Ideal para balancear a carga de trabalho entre vários trabalhadores.
- **Processamento Paralelo:** Utilizado para dividir o processamento de grandes volumes de dados entre vários consumidores.

---

### [Fanout](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Fanout)

#### Explicação
A exchange do tipo Fanout envia todas as mensagens recebidas para todas as filas ligadas a ela, sem considerar qualquer chave de roteamento. Este padrão é útil para casos onde uma mensagem precisa ser entregue a múltiplos consumidores.

<img alt="#" title="RabbitMQ-Fanout" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Fanout.png?alt=media&token=968e9e13-e6f1-4135-91d2-2e6681b76599" />

#### Casos de Uso
- **Broadcast de Mensagens:** Ideal para enviar notificações ou atualizações a todos os sistemas conectados.
- **Sistemas de Log:** Utilizado para enviar logs a diferentes sistemas de análise e armazenamento simultaneamente.

---

### [Direct](https://github.com/Paulo-Ricard0/RabbitMQ-Estudos/tree/main/RMQ-Direct)

#### Explicação
A exchange do tipo Direct roteia mensagens para filas com base em uma chave de roteamento específica. Apenas as filas que estão vinculadas à exchange com a mesma chave de roteamento receberão a mensagem.

<img alt="#" title="RabbitMQ-Direct" src="https://firebasestorage.googleapis.com/v0/b/uploads-58ebc.appspot.com/o/RabbitMQ-Direct.png?alt=media&token=4b7ff07c-1e08-43c4-8185-9411b9a9bc0c" />

#### Casos de Uso
- **Roteamento de Mensagens:** Utilizado quando há necessidade de enviar mensagens a consumidores específicos com base em critérios definidos.
- **Filtragem de Logs:** Permite a separação de logs em diferentes níveis (erro, aviso, info) e envio a diferentes filas para processamento especializado.
