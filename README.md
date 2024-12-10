
# PongIA

PongIA es una versión del clásico juego Pong con inteligencia artificial y un sistema de control avanzado para la velocidad de la bola.

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