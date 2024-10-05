# FDV_Scripts_Events

## **1. Implementar una UI que permita configurar con qué velocidad te moverás: turbo o normal. También debe mostar la cantidad de objetos recolectados y si chocas con alguno especial restar fuerza.**

Creamos un objeto de tipo `Canvas` y cambiamos el `UI Scale Mode` a _"Scale With Screen Size"_ para adaptar el tamaño de la UI a distintas pantallas y la resolución a de referencia (`Reference Resolution`) a 1920x1080 en el componente [`Canvas Scaler`](https://docs.unity3d.com/2022.3/Documentation/Manual/script-CanvasScaler.html).

![image](https://github.com/user-attachments/assets/f985b557-f0d7-45dc-b2b3-687dda4cc6d3)

A continuación, importamos el paquete [_"Starter Assets"_](https://assetstore.unity.com/packages/essentials/starter-assets-thirdperson-updates-in-new-charactercontroller-pa-196526) para implementar la lógica de movimiento del personaje que controlaremos. Luego, añadimos un plano y una cápsula en la escena. A la cápsula le añadiremos los componentes `CharacterController`, `PlayerInput`, `ThirdPersonController`, y `StarterAssetsInputs`.

![Ejercicio 1](https://github.com/user-attachments/assets/721daeb1-578d-4422-962d-1fe8572b809f)

Ahora que tenemos un personaje controlable, añadiremos dos botones para configurar la velocidad del personaje.

![image](https://github.com/user-attachments/assets/bc901711-443a-475d-be01-29f28704d096)

Tras esto tendremos que crear la lógica para modificar la velocidad del personaje. Para ello, crearemos un script con las siguientes variables:

```c#
[SerializeField] private ThirdPersonController _thirdPersonController;
    
private float _normalSpeed;
private float _turboSpeed;

private void Start()
{
  _normalSpeed = _thirdPersonController.MoveSpeed;
  _turboSpeed = _thirdPersonController.MoveSpeed * 2;
}
```

`_thirdPersonController` guardará una referencia al componente que necesitamos para modificar la velocidad del personaje, mientras que `_normalSpeed` y `_turboSpeed` indicará la velocidad en ambos modos.

Para terminar con el script, crearemos los dos métodos públicos que más adelante asignaremos al evento de los botones. El primero de los métodos será `SetSpeedModeToNormal()` y el otro `SetSpeedModeToTurbo()`.

```c#
public void SetSpeedModeToNormal()
{
    _thirdPersonController.MoveSpeed = _normalSpeed;
    Debug.Log("NORMAL SPEED");
}

public void SetSpeedModeToTurbo()
{
    _thirdPersonController.MoveSpeed = _turboSpeed;
    Debug.Log("TURBO SPEED");
}
```

Ahora que se tenemos los métodos necesarios para cambiar el modo de velocidad, se lo asignamos a cada botón desde el inspector.

![image](https://github.com/user-attachments/assets/e0009b8a-169c-492e-aa05-5f43e95556f5)

![image](https://github.com/user-attachments/assets/4026b448-83bb-4116-8c72-195518207178)

_Script completo:_

```c#
using StarterAssets;
using UnityEngine;

public class SpeedModeSelector : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _thirdPersonController;
    
    private float _normalSpeed;
    private float _turboSpeed;

    private void Start()
    {
        _normalSpeed = _thirdPersonController.MoveSpeed;
        _turboSpeed = _thirdPersonController.MoveSpeed * 2;
    }

    public void SetSpeedModeToNormal()
    {
        _thirdPersonController.MoveSpeed = _normalSpeed;
        Debug.Log("NORMAL SPEED");
    }

    public void SetSpeedModeToTurbo()
    {
        _thirdPersonController.MoveSpeed = _turboSpeed;
        Debug.Log("TURBO SPEED");
    }
}
```

_Resultado:_

![Resultado](https://github.com/user-attachments/assets/1ff4eaa0-1358-44db-82a2-5088921e11eb)

## **2. Agrega un personaje que al pinchar sobre un botón de la UI se dirija hacia un objetivo estático en la escena.**

Montaremos la escena añadiendo un único botón, un cubo y otro GameObject que serrá el objetivo.

![image](https://github.com/user-attachments/assets/1e8e0f17-8265-40c1-b6f6-9761370db2a3)

Ahora, crearemos un script y se lo asignaremos al cubo rojo, que será el GameObject que se moverá hacia el objetivo indicado. Creamos las siguientes variables:

* `_target` será el objetivo al que se moverá el cubo.
* `_movementSpeed` indicará la velocidad de movimiento.
* `_accuracy` evitará el _jiterring_ cuando el cubo alcanze al objetivo.
* `_distance` será la distancia entre la posición actual y la del objetivo.
* `_enableMovement` habilitará el movimiento la lógica de movimiento.

```c#
[SerializeField] private Transform _target;

[SerializeField] private float _movementSpeed = 2.0f;
[SerializeField] private float _accuracy = .01f;

private Vector3 _distance;
private bool _enableMovement;
```

![image](https://github.com/user-attachments/assets/499506d1-5403-4fd4-9f06-1e05da7706fe)

Ahora, en el `Update()` creamos la lógica para moverse hacia el objetivo.

```c#
private void Update()
{
    if (!_enableMovement)
        return;
    
    _distance = _target.position - transform.position;
    _distance.y = 0;
    
    if (!(_distance.magnitude > _accuracy))
        return;
    
    transform.Translate(_distance.normalized * (_movementSpeed * Time.deltaTime), Space.World);
}
```
Y finalmente, creamos el método que habilitará el movimiento. Este método será llamado desde el evento del botón.

```c#
public void GoToTarget()
{
    _enableMovement = true;
}
```

![image](https://github.com/user-attachments/assets/d00bb49c-8ff2-44e4-8b54-3578dd02a0ca)

_Scriopt completo:_

```c#
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _movementSpeed = 2.0f;
    [SerializeField] private float _accuracy = .01f;

    private Vector3 _distance;
    private bool _enableMovement;
    
    private void Update()
    {
        if (!_enableMovement)
            return;
        
        _distance = _target.position - transform.position;
        _distance.y = 0;
        
        if (!(_distance.magnitude > _accuracy))
            return;
        
        transform.Translate(_distance.normalized * (_movementSpeed * Time.deltaTime), Space.World);
    }

    public void GoToTarget()
    {
        _enableMovement = true;
    }
}
```

_Resultado:_

![Resultado](https://github.com/user-attachments/assets/08e52448-64fe-4033-956d-4a7b15e7085f)

## **3. Agregar a tu escena un objeto que al ser recolectado por el jugador haga que otros dos objetos en la escena realicen las siguientes acciones: el objeto A se desplaza hacia el jugador. El objeto B se ubica en un punto fijo en la escena.**

Para este ejercicio se ha creado una escena que cuenta con una esfera roja que representa el objeto a recolectar, un cubo amarillo que representa al jugador y dos rombos blancos que representan los objetos A y B.

![image](https://github.com/user-attachments/assets/c20aa7ca-1f73-419d-8217-b8aa8fd8c2fc)

Además de estos cuatro objetos, se ha creado uno adicional al que se le ha desactivado el componente `MeshRenderer` y que será el punto fijo al que se ubicará el objeto B.

![image](https://github.com/user-attachments/assets/23aea165-4595-4491-ba40-44881e04b10e)

Por último, se ha añadido la etiqueta _"Item"_ a la esfera.

![image](https://github.com/user-attachments/assets/a584f0ca-c28e-4e12-af61-dab71838cbaa)

Pasando ahora a la lógica de este ejercicio, se ha desarrollado dos scripts. El primero de ellos es `ItemCollector` y será vinculado al jugador.

![image](https://github.com/user-attachments/assets/82d55ca2-8c14-4028-bba8-a218557a8d42)

Este script cuenta con un evento público que será invocado cada vez que el jugador entra en contacto con un objeto que tenga la etiqueta _"Item"_.

```c#
using System;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public static event Action OnItemCollected;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag.Equals("Item"))
        {
            OnItemCollected?.Invoke();
            Debug.Log("[!] ITEM COLLECTED [!]");
        }
    }
}
```

El segundo script es `Chaser` y será añadido a los objetos A y B. Su contenido es bastante similar al script `CubeMovement` creado para el ejercicio anterior, con la única salvedad de que en el `Start()` el objeto se suscribe al evento `OnItemCollected`.

```c#
using System;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _movementSpeed = 2f;
    [SerializeField] private float _accuracy = .01f;

    private Vector3 _distance;
    private bool _enableMovement;
    
    private void Start()
    {
        ItemCollector.OnItemCollected += GoToTarget;
    }

    private void Update()
    {
        if (!_enableMovement)
            return;

        _distance = _target.position - transform.position;
        _distance.y = 0;
        
        if (!(_distance.magnitude > _accuracy))
            return;
         
        transform.Translate(_distance.normalized * (_movementSpeed * Time.deltaTime), Space.World);
    }

    private void GoToTarget()
    {
        _enableMovement = true;
        Debug.Log($"{gameObject.name} triggered");
    }

    private void OnDestroy()
    {
        ItemCollector.OnItemCollected -= GoToTarget;
    }
}
```

![image](https://github.com/user-attachments/assets/7a574d78-cfb0-4eb1-b9f0-852ed97f6a64)

_Resultado:_

![3  Resultado](https://github.com/user-attachments/assets/0993c30a-5441-415a-8f27-e3a87f8f3dee)
