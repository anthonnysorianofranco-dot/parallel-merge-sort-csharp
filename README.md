# Parallel Merge Sort in C#

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
- `/metrics` → Resultados de rendimiento y gráficas

---

## Equipo

- Anthonny Brayhan Soriano Franco (Líder)

---

## Estado del Proyecto

- [x] Tema aprobado
- [x] Implementación secuencial
- [x] Implementación paralela
- [x] Benchmarks de rendimiento
- [x] Documentación final

---

## Resumen de Rendimiento

La implementación paralela se evaluó utilizando un procesador de 4 núcleos.

La mejor configuración fue **`MaxDepth = 2`**, logrando:

- Tiempo promedio de ejecución paralela: **186.60 ms**
- Tiempo promedio de ejecución secuencial: **331.20 ms**
- Speedup: **1.77x**
- Eficiencia: **44.37%**

Los resultados mostraron que aumentar la profundidad de recursión más allá de 2 no mejoró el rendimiento debido al sobrecoste (*overhead*) adicional de la gestión de tareas.

El benchmark de escalabilidad también mostró mejoras constantes en el rendimiento para tamaños de entrada desde 100,000 hasta 2,000,000 de elementos.