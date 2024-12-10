

# PongIA

PongIA es una versión del clásico juego Pong con inteligencia artificial y un sistema de control avanzado para la velocidad de la bola.

# Índice

- [1. Aceleración](#1-aceleración)
  - [Descripción](#descripción)
  - [Cómo funciona](#cómo-funciona)
  - [Comportamiento de cada tipo de movimiento](#comportamiento-de-cada-tipo-de-movimiento)
  - [Modificaciones](#modificaciones)
- [2. Máquina de Estados](#2-máquina-de-estados)
  - [Estados de la Bola](#estados-de-la-bola)
  - [Código de control de los estados de la bola](#código-de-control-de-los-estados-de-la-bola)


# 1. Aceleración

## Descripción

En este juego de Pong, la bola tiene tres tipos de movimientos definidos por un `enum MovementType`, el cual permite elegir entre tres modos de velocidad para la bola:

1. **Basic** (Básico): Movimiento estándar sin ninguna aceleración. La bola se mueve a una velocidad constante.

2. **Linear** (Lineal): En este modo, la velocidad de la bola aumenta de forma lineal con un multiplicador de velocidad. La fórmula utilizada para calcular la velocidad en cada fotograma es la siguiente:
   

    ballRb.velocity = ballRb.velocity.normalized * initialVelocity;

Donde `initialVelocity` es la velocidad inicial de la bola y `ballRb.velocity.normalized` asegura que la dirección de la bola sea mantenida constante mientras se ajusta su magnitud.

3. **Exponential** (Exponencial): La velocidad de la bola aumenta exponencialmente con el tiempo. Este modo aplica una fórmula exponencial en cada actualización que incrementa la velocidad en función del tiempo transcurrido:

    float speed = ballRb.velocity.magnitude;
    float exponentialFactor = Mathf.Pow(1 + velocityMultiplier, Time.deltaTime);
    ballRb.velocity = ballRb.velocity.normalized * speed * exponentialFactor;

Aquí, velocityMultiplier es el factor que controla el aumento exponencial de la velocidad, y Time.deltaTime asegura que el incremento sea proporcional al tiempo transcurrido entre cada fotograma.

## Cómo funciona
En el script Ball.cs, el tipo de movimiento se selecciona mediante el enum MovementType. Durante la actualización (Update()), el juego aplica la lógica correspondiente a cada tipo de movimiento:

    private void Update()
    {
        // Update velocity based on movement type
        switch (movementType)
        {
            case MovementType.Basic:
                // Basic movement, no additional changes
                break;
    
            case MovementType.Linear:
                // Keep the velocity constant with multiplier growth
                ballRb.velocity = ballRb.velocity.normalized * initialVelocity;
                break;
    
            case MovementType.Exponential:
                // Apply exponential scaling to the velocity
                float speed = ballRb.velocity.magnitude;
                float exponentialFactor = Mathf.Pow(1 + velocityMultiplier, Time.deltaTime);
                ballRb.velocity = ballRb.velocity.normalized * speed * exponentialFactor;
                break;
        }
    
        // Check if the ball is out of bounds
        CheckOutOfBounds();
    }

#### Cada tipo de movimiento tiene un comportamiento diferente:

**Basic:** No se aplica ninguna fórmula adicional, la bola mantiene una velocidad constante. Y incrementa al chocar con los paddles.
**Linear:** La velocidad de la bola es constante pero con un crecimiento gradual dependiendo de velocityMultiplier.
**Exponential:** La velocidad de la bola crece de manera exponencial a medida que pasa el tiempo.

##  Modificaciones
Puedes modificar el tipo de movimiento de la bola desde el Inspector de Unity, seleccionando el tipo de movimiento deseado (Basic, Linear o Exponential). Ajusta el valor de initialVelocity para cambiar la velocidad inicial de la bola, y velocityMultiplier para controlar la rapidez del aumento de la velocidad en los modos Lineal y Exponencial.

# 2. Máquina de estados

## Estados de la Bola
La bola en PongIA tiene dos estados principales que determinan su comportamiento: Idle (Inactivo) y Moving (En Movimiento). Esto se controla a través del método Update() en el script de la bola. Dependiendo de la zona en la que se encuentre la bola, se decide si se mueve hacia el centro o persigue su trayectoria.

**Idle (Inactivo):** Este es el estado en el que la bola está en reposo o en una zona específica del campo de juego. En este estado, la bola no se mueve hasta que es necesario.

**Moving (En Movimiento):** Cuando la bola se encuentra en la zona activa (determinada por la función IsballInArea1()), la bola comienza a moverse, siguiendo la lógica de control de velocidad correspondiente a su tipo de movimiento (Básico, Lineal o Exponencial).

### Código de control de los estados de la bola
En el script Ball.cs, el método Update() se encarga de verificar si la bola está en el área 1 (zona activa) o en el área 2 (zona inactiva). Dependiendo de esto, la bola persigue su trayectoria o se mueve hacia el centro:

    void Update()
    {
        if (GameManager.Instance.IsballInArea1())
        {
            ChaseBall();  // Persigue la trayectoria de la bola
        }
        else
        {
            MoveToCenter();  // Mueve la bola hacia el centro
        }
    }

*ChaseBall():* Hace que la bola persiga su trayectoria según la lógica del tipo de movimiento elegido (Básico, Lineal o Exponencial).
*MoveToCenter():* Si la bola no está en la zona activa, se mueve de vuelta al centro de la pantalla.

Este sistema permite tener una mayor flexibilidad en el comportamiento de la bola, adaptándose a las necesidades del juego mientras cambia entre los diferentes tipos de movimiento.