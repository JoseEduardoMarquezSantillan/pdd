
Los diagramas de Mermaid son una forma de representar gráficos y diagramas utilizando una sintaxis de texto simple y declarativa, que luego se puede transformar en una representación visual. Mermaid es una herramienta que facilita la creación de diagramas dinámicos y estáticos para la documentación de software y otras necesidades de visualización, todo dentro de archivos de marcado como Markdown, sin la necesidad de herramientas externas de gráficos o diagramas.

En cuanto a los diagramas de clases, que son una parte esencial de la modelación en la ingeniería de software, especialmente cuando se trabaja con el paradigma de la Programación Orientada a Objetos (POO), Mermaid ofrece una sintaxis para definir clases, junto con sus propiedades y métodos, así como las relaciones entre ellas, como la herencia, la asociación, la composición y la agregación.

Sin embargo, Mermaid no es tan extenso en la modelación de diagramas de clases como otras herramientas especializadas como UML (Unified Modeling Language) y sus herramientas dedicadas. Mermaid se centra más en la facilidad y accesibilidad, integrándose bien con entornos que soportan Markdown, lo que lo hace muy útil para la documentación en GitHub, GitLab, y otras plataformas similares.










## Patrones de diseño creacional
1. Singleton 
```mermaid
classDiagram
    direction LR
    class Singleton {
        -Singleton instance$
        -Singleton()
        +getInstance()$ Singleton
    }

    class Client

    Client --> Singleton : use
    Singleton --> Singleton : create, has
```
2. Factory Method
```mermaid
classDiagram
  direction BT
  class Creator{
    <<abstract>>
    +someOperation()
    +createProduct()* Product
  }
  class Product{
    <<interface>>
    +doStuff()
  }
  class ConcreteCreatorA{
    +createProduct() Product
  }
  class ConcreteCreatorB{
    +createProduct() Product
  }
  class ConcreteProductA{
  }
  class ConcreteProductB{
  }
  
  ConcreteCreatorA --|> Creator
  ConcreteCreatorB --|> Creator
  Creator --> Product : Create 
  ConcreteProductA ..|> Product
  ConcreteProductB ..|> Product
```
3. Abstract Factory
```mermaid
classDiagram
  class AbstractProductA {
    <<abstract>>
  }
  AbstractProductA <|-- ConcreteProductA1
  ConcreteProductA2 --|> AbstractProductA

  class AbstractProductB {
    <<abstract>>
  }
  AbstractProductB <|-- ConcreteProductB1
  ConcreteProductB2 --|> AbstractProductB

  class ConcreteFactory1 {
    +createProductA() ProductA
    +createProductB() ProductB
  }
  class AbstractFactory {
    <<interface>>
    +createProductA() ProductA
    +createProductB() ProductB    
  }
  class ConcreteFactory2 {
    +createProductA() ProductA
    +createProductB() ProductB
  }

  AbstractFactory <|.. ConcreteFactory1
  ConcreteFactory2 ..|> AbstractFactory
  
  class Client {
    -factory: AbstractFactory
    +Client(f: AbstractFactory)
    +someOperation()
  }

  ConcreteFactory1 ..> ConcreteProductB1
  ConcreteFactory1 ..> ConcreteProductA1

  ConcreteFactory2 ..> ConcreteProductB2
  ConcreteFactory2 ..> ConcreteProductA2

  Client --> AbstractFactory
```
4. Builder
```mermaid
classDiagram
    direction BT
    class Builder {
        <<interface>>
        +reset()
        +buildStepA()
        +buildStepB()
        +buildStepC()
    }

    class ConcreteBuilder1 {
        -Product1 result
        +reset()
        +buildStepA()
        +buildStepB()
        +buildStepC()
        +getResult() Product1
    }

    class ConcreteBuilder2 {
        -Product2 result
        +reset()
        +buildStepA()
        +buildStepB()
        +buildStepC()
        +getResult() Product2
    }

    class Director {
        -Builder builder
        +Director(builder)
        +changeBuilder(builder)
        +make(type)
    }

    class Product1
    class Product2

    Product1 <-- ConcreteBuilder1: create
    Product2 <-- ConcreteBuilder2: create
    ConcreteBuilder1 ..|> Builder
    ConcreteBuilder2 ..|> Builder
    Director o--> Builder
```
5. Prototype
```mermaid
  classDiagram
    direction BT
    class Prototype{
      <<interface>>
      +clone() Prototype
    }

    class ConcretePrototype{
      -field1
      +ConcretePrototype(prototype)
      +clone() Prototype
    }

    class SubclassPrototype{
      -field2
      +SubclassPrototype(prototype)
      +clone() SubclassPrototype
    }
    
    class Client
    SubclassPrototype --|> ConcretePrototype
    ConcretePrototype ..|> Prototype
    Client --> Prototype : use
```
## Patrones de diseño estructural
6. Adapter 
```mermaid
classDiagram
  direction BT
  class Client
  class ClientInterface {
    <<interface>>
    +method(data)
  }
  class Adapter {
    -adaptee: Service
    +method(data)
  }
  class Service {
    +serviceMethod(specialData)
  }
  
  Client --> ClientInterface: use
  Adapter ..|> ClientInterface
  Adapter o-->Service

```
7. Decorator
```mermaid
classDiagram
  direction BT
  class Client
  class Component {
    <<interface>>
    +execute()
  }
  class ConcreteComponent {
    +execute()
  }
  class BaseDecorator {
    -wrappee: Component
    +BaseDecorator(c: Component)
    +execute()
  }
  class ConcreteDecorators {
    +execute()
    +extra()
  }
  
  Client --> Component: use
  ConcreteComponent ..|> Component
  BaseDecorator ..|> Component
  ConcreteDecorators --|> BaseDecorator
  BaseDecorator o--> Component
```
8. Facade
```mermaid
classDiagram
  direction LR
  class Client
  class Facade {
    -linksToSubsystemObjects
    -optionalAdditionalFacade
    +subsystemOperation()
  }
  class AdditionalFacade {
    +anotherOperation()
  }
  Client --> Facade
  Facade --> AdditionalFacade
```
9. Proxy
```mermaid
classDiagram
  direction BT
  class ServiceInterface {
    <<interface>>
    +operation()
  }
  class Proxy {
    -realService: Service
    +Proxy(s: Service)
    +checkAccess()
    +operation()
  }
  class Service {
    +operation()
  }
  Client --> ServiceInterface
  Proxy ..|> ServiceInterface
  Service ..|> ServiceInterface
  Proxy o--> Service
```
## Patrones de diseño de comportamiento
10. Chain of Responsibility
```mermaid
classDiagram
  direction BT
  class Handler {
      <<interface>>
      +setNext(h: Handler)
      +handle(request)
  }

  class BaseHandler {
      <<abstract>>
      -next: Handler
      +setNext(h: Handler)
      +handle(request)
  }

  class ConcreteHandlers {
      +handle(request)
  }

  BaseHandler o--> Handler
  BaseHandler ..|> Handler
  ConcreteHandlers --|> BaseHandler
  Client --> Handler
```
11. Iterator
```mermaid
classDiagram
    direction BT
    class Client
    class Iterator {
        <<interface>>
        +getNext()
        +hasMore() bool
    }
    class IterableCollection {
        <<interface>>
        +createIterator() Iterator
    }
    class ConcreteIterator {
        -collection: ConcreteCollection
        -iterationState
        +ConcreteIterator(c: ConcreteCollection)
        +getNext()
        +hasMore() bool
    }
    class ConcreteCollection {
        +createIterator() Iterator
    }
    Iterator <-- Client: use
    IterableCollection <-- Client: use
    ConcreteIterator ..|> Iterator
    ConcreteCollection ..|> IterableCollection
    ConcreteIterator <--> ConcreteCollection
```
12. Observer
```mermaid
classDiagram
  direction LR
  class Publisher {
      -subscribers: Subscriber[]
      -mainState
      +subscribe(s: Subscriber)
      +unsubscribe(s: Subscriber)
      +notifySubscribers()
      +mainBusinessLogic()
  }
  class Subscriber {
      <<interface>>
      +upgrade(context)
  }
  class ConcreteSubscribers {
      +update(context)
  }
  class Client

  Publisher o--> Subscriber
  ConcreteSubscribers ..|> Subscriber
  Client --> Publisher
  Client ..> ConcreteSubscribers
```
