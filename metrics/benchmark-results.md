# Resultados de Benchmark

## Entorno de Pruebas

- Lenguaje: C#
- Framework: .NET 10
- Paralelización: Task Parallel Library (TPL)
- Cantidad de procesadores: 4
- Tamaño de entrada para el análisis de profundidad: 1,000,000 elementos
- Ejecuciones por benchmark: 5

## Resultados de Escalabilidad

| Tamaño de entrada | Promedio Secuencial (ms) | Promedio Paralelo (ms) | Speedup | Efficiency |
|------------------:|-------------------------:|-----------------------:|--------:|-----------:|
| 100,000           | 31.33                    | 19.33                  | 1.62x   | 40.52%     |
| 500,000           | 169.33                   | 93.00                  | 1.82x   | 45.52%     |
| 1,000,000         | 340.67                   | 189.33                 | 1.80x   | 44.98%     |
| 2,000,000         | 686.33                   | 390.00                 | 1.76x   | 44.00%     |

## Análisis de Profundidad Paralela

| MaxDepth | Promedio Secuencial (ms) | Promedio Paralelo (ms) | Speedup | Efficiency |
|---------:|-------------------------:|-----------------------:|--------:|-----------:|
| 1        | 369.40                   | 272.00                 | 1.36x   | 33.95%     |
| 2        | 351.80                   | 238.40                 | 1.48x   | 36.89%     |
| 3        | 331.60                   | 198.00                 | 1.67x   | 41.87%     |

## Análisis

La implementación paralela superó consistentemente a la implementación secuencial en el benchmark de escalabilidad y en las configuraciones de profundidad evaluadas.

El benchmark de escalabilidad mostró un Speedup entre 1.62x y 1.82x. El mayor Speedup se obtuvo con 500,000 elementos, alcanzando 1.82x con una Efficiency de 45.52%.

Para 1,000,000 de elementos, el benchmark de escalabilidad anterior obtuvo un tiempo promedio de ejecución de 189.33 ms para la implementación paralela, frente a 340.67 ms para la implementación secuencial.

Para 2,000,000 de elementos, la implementación secuencial necesitó un promedio de 686.33 ms, mientras que la implementación paralela necesitó 390.00 ms.

El análisis de profundidad realizado durante la validación final mostró que MaxDepth 3 obtuvo el mejor resultado en esta ejecución, con un tiempo promedio paralelo de 198.00 ms, un Speedup de 1.67x y una Efficiency de 41.87%.

Con MaxDepth 1 se obtuvo un Speedup de 1.36x, mientras que MaxDepth 2 obtuvo 1.48x. Al aumentar la profundidad hasta MaxDepth 3, el rendimiento medido mejoró en esta ejecución.

Estos resultados también demuestran que los benchmarks pueden presentar variaciones entre diferentes ejecuciones debido a factores como la carga del procesador, la planificación del sistema operativo y el overhead asociado a la administración de tareas.

La implementación paralela aprovecha los cuatro núcleos disponibles del procesador. Sin embargo, el Speedup no alcanza el máximo teórico debido al overhead producido por la administración de tareas, la sincronización, el acceso a memoria y las operaciones de Merge.

## Conclusión

Los resultados obtenidos demuestran que la aplicación de paralelismo mediante Task Parallel Library (TPL) permite reducir el tiempo de ejecución del algoritmo Merge Sort para los tamaños de entrada evaluados.

La implementación paralela mostró mejoras de rendimiento frente a la versión secuencial, especialmente cuando el tamaño del problema aumenta.

El análisis de profundidad también permitió observar que el nivel de paralelismo influye directamente en el rendimiento. Una mayor cantidad de tareas no garantiza necesariamente un mejor resultado, debido al overhead adicional generado por su administración y sincronización.

Durante la validación final, MaxDepth 3 obtuvo el mejor resultado entre las configuraciones evaluadas. No obstante, este resultado corresponde a una ejecución específica y puede variar debido a las condiciones del sistema durante cada benchmark.

En general, las pruebas confirman que el algoritmo Parallel Merge Sort puede aprovechar los recursos de un procesador de cuatro núcleos y proporcionar una mejora significativa respecto a la implementación secuencial.