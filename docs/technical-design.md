# Technical Design

## 1. General Description

Parallel Merge Sort es una aplicación desarrollada en C# que compara una implementación secuencial de Merge Sort con una implementación paralela utilizando la Task Parallel Library (TPL).

La implementación paralela aplica descomposición recursiva para dividir el problema de ordenamiento en subproblemas independientes más pequeños.

El objetivo principal es evaluar las ventajas y limitaciones de la ejecución paralela utilizando:

- Tiempo de ejecución.
- Speedup (Aceleración).
- Eficiencia.
- Escalabilidad.
- Diferentes niveles de paralelismo.

---

## 2. System Architecture

El proyecto está organizado en varios componentes:

### `Program.cs`
El punto de entrada de la aplicación.

**Responsabilidades:**
- Generar datos de entrada.
- Ejecutar el algoritmo secuencial.
- Ejecutar el algoritmo paralelo.
- Medir el tiempo de ejecución.
- Calcular el Speedup y la Eficiencia.
- Ejecutar benchmarks de rendimiento.
- Validar resultados.

### `MergeSorter.cs`
Contiene la implementación secuencial de Merge Sort.

La clase realiza la división recursiva del arreglo y utiliza la operación `Merge` para combinar las porciones ordenadas.

### `ParallelMergeSorter.cs`
Contiene la implementación paralela.

**Características principales:**
- Descomposición recursiva.
- USO de `Task.Run`.
- Ejecución concurrente de ramas izquierda y derecha.
- Profundidad máxima configurable.
- Sincronización mediante `Task.WaitAll`.

### `ParallelMergeSort.Tests`
Contiene pruebas automatizadas con xUnit utilizadas para verificar la corrección de los algoritmos de ordenamiento.

### `metrics/`
Contiene los resultados de benchmarks e información de rendimiento.

---

## 3. Recursive Decomposition

Merge Sort sigue la estrategia de "divide y vencerás".

El algoritmo divide repetidamente el arreglo en dos mitades:

                 Array
                /     \
               /       \
          Left half   Right half
             |             |
          Divide          Divide
             |             |
          Smaller       Smaller
          problems      problems

Después de que finalizan las operaciones recursivas de ordenamiento, las dos porciones ordenadas se fusionan.

La versión paralela aprovecha el hecho de que las porciones izquierda y derecha se pueden procesar de forma independiente.

---

## 4. Parallelization Strategy

La implementación paralela utiliza `Task` para ejecutar las dos ramas recursivas concurrentemente.

La estrategia principal es:

                Array
               /     \
              /       \
       Left branch   Right branch
           |             |
        Task.Run      Task.Run
           |             |
           +------+------+
                  |
             Task.WaitAll
                  |
                Merge

La implementación no crea tareas en cada nivel de recursión. En su lugar, utiliza un `maxDepth` configurable.

Cuando la profundidad actual está por debajo de la profundidad máxima, las ramas izquierda y derecha se ejecutan utilizando tareas separadas.

Cuando se alcanza la profundidad máxima, las operaciones recursivas restantes se ejecutan secuencialmente. Esto limita el sobrecoste (*overhead*) de creación de tareas.

---

## 5. Maximum Parallel Depth

La profundidad máxima paralela controla cuánta concurrencia se introduce en el algoritmo.

La aplicación utiliza una profundidad máxima de 2 para el benchmark principal de escalabilidad, ya que la máquina de prueba cuenta con cuatro procesadores lógicos.

La relación se puede representar como:

Depth 0
        |
        +----------------+
        |                |
     Depth 1          Depth 1
      /   \            /   \
     /     \          /     \
   Depth 2 Depth 2  Depth 2 Depth 2

Esto crea cuatro porciones recursivas primarias.

Utilizar una profundidad excesiva podría crear más tareas de las que el procesador puede ejecutar eficientemente, aumentando el sobrecoste de planificación y sincronización de tareas.

---

## 6. Synchronization

La implementación paralela debe garantizar que ambas ramas recursivas hayan completado antes de fusionar sus resultados.

Esto se logra utilizando:

`Task.WaitAll(leftTask, rightTask);`

La secuencia de sincronización es:

Left Task  --------\
                    \
                     >---- WaitAll ----> Merge
                    /
Right Task --------/

`Task.WaitAll` evita que la operación de fusión comience antes de que ambas ramas hayan terminado.

---

## 7. Merge Operation

La operación `Merge` combina dos porciones del arreglo que ya están ordenadas.

Por ejemplo:
- Izquierda: `[1, 4, 7]`
- Derecha: `[2, 3, 8]`
- Resultado: `[1, 2, 3, 4, 7, 8]`

La operación de fusión utiliza un arreglo temporal para almacenar resultados intermedios antes de copiarlos de regreso al arreglo original.

Tanto la implementación secuencial como la paralela comparten la misma lógica de fusión.

---

## 8. Complexity Analysis

Merge Sort tiene una complejidad temporal de:

$$O(n \log n)$$

para los casos mejor, promedio y peor.

El almacenamiento temporal adicional requerido por el algoritmo es:

$$O(n)$$

La versión paralela mantiene la misma complejidad computacional general, pero distribuye el trabajo recursivo independiente entre los recursos del procesador.

La mejora práctica del rendimiento depende de:
- Número de núcleos del procesador.
- Tamaño de la entrada.
- Sobrecoste de planificación de tareas.
- Sobrecoste de sincronización.
- Operaciones de fusión.
- Profundidad máxima paralela.

---

## 9. Performance Metrics

La aplicación calcula tres métricas principales de rendimiento:

- **Tiempo de Ejecución:** El tiempo transcurrido necesario para ordenar la entrada.
- **Speedup (Aceleración):**
  $$\text{Speedup} = \frac{\text{Tiempo Secuencial}}{\text{Tiempo Paralelo}}$$
  Por ejemplo, si la implementación secuencial toma 400 ms y la paralela 200 ms:
  $$\text{Speedup} = \frac{400}{200} = 2.0x$$
- **Eficiencia:**
  $$\text{Eficiencia} = \frac{\text{Speedup}}{\text{Número de Procesadores}} \times 100$$
  Con cuatro procesadores y un Speedup de 2.0x:
  $$\text{Eficiencia} = \frac{2.0}{4} \times 100 = 50\%$$

---

## 10. Benchmark Design

El benchmark ejecuta múltiples iteraciones para cada configuración.

Para las pruebas de escalabilidad, la aplicación utiliza tamaños de:
- 100,000 elementos.
- 500,000 elementos.
- 1,000,000 elementos.
- 2,000,000 elementos.

Para las pruebas de profundidad de paralelización, la aplicación compara:
- `MaxDepth = 1`
- `MaxDepth = 2`
- `MaxDepth = 3`

Se realizan múltiples ejecuciones para reducir el efecto de las variaciones individuales de ejecución, utilizando el tiempo medio de ejecución para la comparación principal.

---

## 11. Testing Strategy

Las pruebas automatizadas están implementadas con xUnit.

La suite de pruebas verifica la corrección de los algoritmos de ordenamiento utilizando diferentes tipos de entrada:
- Arreglos vacíos.
- Arreglos de un solo elemento.
- Arreglos ya ordenados.
- Arreglos ordenados a la inversa.
- Arreglos con valores duplicados.
- Valores aleatorios.

Actualmente, la suite reporta:
- Pruebas totales: 10
- Pasadas: 10
- Falladas: 0
- Omitidas: 0

Esto confirma que las operaciones de ordenamiento producen resultados correctamente ordenados para los casos probados.

---

## 12. Scalability

La implementación paralela se evaluó utilizando tamaños de entrada crecientes.

El benchmark produjo los siguientes promedios:

| Input Size | Sequential | Parallel | Speedup | Efficiency |
| ---------: | ---------: | -------: | ------: | ---------: |
|    100,000 |   31.33 ms | 19.33 ms |   1.62x |     40.52% |
|    500,000 |  169.33 ms | 93.00 ms |   1.82x |     45.52% |
|  1,000,000 |  340.67 ms |189.33 ms |   1.80x |     44.98% |
|  2,000,000 |  686.33 ms |390.00 ms |   1.76x |     44.00% |

La implementación paralela fue más rápida para todos los tamaños probados.

Los resultados también muestran que el Speedup no aumenta indefinidamente debido a que la ejecución paralela introduce sobrecostes de gestión de tareas, sincronización y fusión.

---

## 13. Limitations

La implementación presenta varias limitaciones:

- **Dependencia del procesador:** El rendimiento depende del número de núcleos disponibles.
- **Sobrecoste de tareas:** La creación y planificación de tareas introduce tiempo adicional.
- **Sobrecoste de sincronización:** Las ramas recursivas deben sincronizarse antes de la operación de fusión.
- **Uso de memoria:** Merge Sort requiere memoria adicional para arreglos temporales.
- **Variabilidad del benchmark:** Los tiempos pueden variar entre ejecuciones debido a la actividad del sistema operativo, procesos en segundo plano y planificación del procesador.

---

## 14. Design Conclusion

El diseño técnico demuestra cómo se puede aplicar la descomposición recursiva a Merge Sort para introducir ejecución paralela.

La implementación utiliza tareas TPL, profundidad de recursión controlada y sincronización para ejecutar porciones independientes del algoritmo concurrentemente.

Los benchmarks y las pruebas automatizadas proporcionan evidencia tanto de la corrección funcional como de la mejora en el rendimiento.