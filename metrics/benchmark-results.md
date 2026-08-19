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
| 1        | 358.20                   | 249.40                 | 1.44x   | 35.91%     |
| 2        | 330.40                   | 185.20                 | 1.78x   | 44.60%     |
| 3        | 333.60                   | 201.60                 | 1.65x   | 41.37%     |

El análisis de profundidad realizado durante la validación final mostró que **MaxDepth 2** obtuvo el mejor resultado en esta ejecución, con un tiempo promedio paralelo de **185.20 ms**, un Speedup de **1.78x** y una Efficiency de **44.60%**.

Con MaxDepth 1 se obtuvo un Speedup de **1.44x**, mientras que MaxDepth 3 obtuvo **1.65x**. Esto demuestra que aumentar la profundidad de paralelización no garantiza necesariamente un mejor rendimiento, debido al overhead asociado a la creación, planificación y sincronización de tareas.

Estos resultados pueden variar entre diferentes ejecuciones debido a factores como la carga del procesador, la planificación del sistema operativo y el overhead asociado a la administración de tareas.

## Conclusión

Durante la validación final, MaxDepth 2 obtuvo el mejor resultado entre las configuraciones evaluadas.