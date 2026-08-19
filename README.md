# Parallel Merge Sort en C#

### Proyecto Final – Programación Paralela

- **Tema:** Descomposición Recursiva
- **Algoritmo:** Parallel Merge Sort
- **Lenguaje:** C# (.NET 10)
- **Tecnología:** Task Parallel Library (TPL)

---

## Objetivo del Proyecto

Implementar y evaluar un algoritmo paralelo recursivo de Merge Sort capaz de ordenar grandes volúmenes de datos, comparando la ejecución secuencial y paralela en términos de:

- Tiempo de ejecución
- Speedup (Aceleración)
- Eficiencia
- Escalabilidad
- Utilización de CPU

---

## Estructura del Repositorio

- `/docs` → Documentación del proyecto
- `/src` → Código fuente
- `/tests` → Pruebas unitarias
- `/metrics` → Resultados de rendimiento

---

## Equipo

- Anthonny Brayhan Soriano Franco (Líder)

---

## Estado del Proyecto

- [x] Tema aprobado
- [x] Implementación secuencial
- [x] Implementación paralela
- [x] Benchmarks de rendimiento
- [x] Pruebas automatizadas
- [x] Documentación técnica
- [x] Documentación del proyecto

---

## Resultados de Rendimiento

La implementación paralela fue evaluada utilizando un procesador de 4 núcleos.

### Análisis de Profundidad

En la ejecución de validación final se obtuvieron los siguientes resultados para 1,000,000 de elementos:

| MaxDepth | Tiempo Secuencial | Tiempo Paralelo | Speedup | Efficiency |
|---------:|------------------:|----------------:|--------:|-----------:|
| 1 | 358.20 ms | 249.40 ms | 1.44x | 35.91% |
| 2 | 330.40 ms | 185.20 ms | 1.78x | 44.60% |
| 3 | 333.60 ms | 201.60 ms | 1.65x | 41.37% |

En esta ejecución, **MaxDepth = 2** obtuvo el mejor rendimiento, alcanzando un Speedup de **1.78x** y una Efficiency de **44.60%**.

Los resultados pueden variar ligeramente entre ejecuciones debido a factores como la carga del procesador, la planificación del sistema operativo y el overhead asociado a la creación y sincronización de tareas.

### Escalabilidad

El benchmark de escalabilidad evaluó tamaños de entrada desde **100,000 hasta 2,000,000 de elementos**.

Los resultados mostraron que la implementación paralela mantuvo una mejora de rendimiento respecto a la versión secuencial a medida que aumentó el tamaño del problema.

El Speedup obtenido durante el benchmark de escalabilidad estuvo entre **1.62x y 1.82x**, demostrando que el algoritmo puede aprovechar los recursos disponibles del procesador para reducir el tiempo de ejecución.

---

## Pruebas Automatizadas

El proyecto incluye pruebas unitarias desarrolladas con **xUnit**.

La última ejecución obtuvo:

- **10 pruebas**
- **10 correctas**
- **0 errores**
- **0 omitidas**

Las pruebas verifican principalmente que las implementaciones secuencial y paralela produzcan correctamente arreglos ordenados.

---

## Conclusión

Los resultados obtenidos demuestran que la aplicación de paralelismo mediante Task Parallel Library (TPL) permite mejorar el rendimiento de Merge Sort para los tamaños de entrada evaluados.

La implementación utiliza descomposición recursiva para dividir el problema en subproblemas que pueden ejecutarse de manera concurrente.

Sin embargo, el Speedup no alcanza el máximo teórico debido al overhead producido por la administración de tareas, sincronización, acceso a memoria y operaciones de Merge.

El proyecto demuestra los beneficios y las limitaciones del paralelismo aplicado a un algoritmo de ordenamiento recursivo.