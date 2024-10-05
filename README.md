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




## **3. Agregar a tu escena un objeto que al ser recolectado por el jugador haga que otros dos objetos en la escena realicen las siguientes acciones: el objeto A se desplaza hacia el jugador. El objeto B se ubica en un punto fijo en la escena.**


