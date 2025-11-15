# Frontend - Módulo de Productos

Este es el módulo frontend para la gestión de productos, desarrollado con React, TypeScript y shadcn/ui.

## Tecnologías Utilizadas

- **React 18** - Librería de UI
- **TypeScript** - Tipado estático
- **Vite** - Build tool y dev server
- **Tailwind CSS v4** - Framework de CSS
- **shadcn/ui** - Componentes de UI
- **Axios** - Cliente HTTP
- **Radix UI** - Primitivas de UI accesibles
- **Lucide React** - Iconos

## Características

### Módulo de Productos

- ✅ Listado de productos en tabla
- ✅ Paginación (configurable, por defecto 10 items por página)
- ✅ Modal de creación de productos
- ✅ Modal de edición de productos
- ✅ Integración completa con API backend

### Campos del Producto

- Código
- Nombre
- Descripción
- Categoría (select)
- Unidad de Medida (select)
- Tipo de IGV (select)
- Precio de Compra
- Precio de Venta

## Requisitos Previos

- Node.js 18+ instalado
- Backend API corriendo en `http://localhost:5121`

## Instalación

```bash
# Instalar dependencias
npm install
```

## Configuración

El proyecto está configurado para conectarse a la API en `http://localhost:5121/api`. Si necesitas cambiar esto, edita el archivo:

```
src/services/api.ts
```

## Comandos Disponibles

### Modo Desarrollo

```bash
npm run dev
```

Inicia el servidor de desarrollo en `http://localhost:7190`

### Build para Producción

```bash
npm run build
```

Genera los archivos optimizados en el directorio `dist/`

### Preview del Build

```bash
npm run preview
```

Previsualiza el build de producción localmente

### Linting

```bash
npm run lint
```

Ejecuta el linter para verificar el código

## Estructura del Proyecto

```
frontend/
├── src/
│   ├── components/
│   │   ├── ui/              # Componentes de shadcn/ui
│   │   │   ├── button.tsx
│   │   │   ├── table.tsx
│   │   │   ├── dialog.tsx
│   │   │   ├── input.tsx
│   │   │   ├── label.tsx
│   │   │   ├── select.tsx
│   │   │   └── pagination.tsx
│   │   └── ProductFormModal.tsx  # Modal de crear/editar producto
│   ├── pages/
│   │   └── ProductsPage.tsx      # Página principal de productos
│   ├── services/
│   │   ├── api.ts                # Cliente Axios configurado
│   │   └── productService.ts     # Servicios de productos
│   ├── types/
│   │   └── product.ts            # Tipos TypeScript
│   ├── lib/
│   │   └── utils.ts              # Utilidades (cn())
│   ├── App.tsx
│   ├── App.css
│   ├── index.css                 # Estilos globales + Tailwind
│   └── main.tsx
├── public/
├── index.html
├── package.json
├── tsconfig.json
├── tsconfig.app.json
├── vite.config.ts
├── tailwind.config.js
└── postcss.config.js
```

## Uso del Módulo de Productos

### Listar Productos

Al cargar la página, se muestran automáticamente los productos en una tabla con:
- Código
- Nombre
- Descripción
- Categoría
- Unidad de Medida
- Tipo de IGV
- Precio de Compra
- Precio de Venta
- Botón de edición

### Crear Producto

1. Click en el botón "Nuevo Producto"
2. Llenar el formulario con todos los datos requeridos
3. Click en "Crear"

### Editar Producto

1. Click en el ícono de lápiz en la fila del producto
2. Modificar los datos en el formulario
3. Click en "Actualizar"

### Paginación

- Usa los botones "Anterior" y "Siguiente" para navegar entre páginas
- Se muestra el número de página actual y total de páginas

## API Endpoints Utilizados

- `GET /api/product?pageNumber={n}&pageSize={n}` - Listar productos
- `POST /api/product` - Crear producto
- `PUT /api/product/{id}` - Actualizar producto
- `GET /api/category` - Listar categorías
- `GET /api/unitofmeasure` - Listar unidades de medida
- `GET /api/igvtype` - Listar tipos de IGV

## Personalización

### Cambiar colores del tema

Edita las variables CSS en `src/index.css`:

```css
:root {
  --primary: 222.2 47.4% 11.2%;
  --secondary: 210 40% 96.1%;
  /* ... más colores */
}
```

### Cambiar tamaño de página

En `src/pages/ProductsPage.tsx`, modifica la constante:

```typescript
const pageSize = 10; // Cambia este valor
```

## Troubleshooting

### Error de CORS

Si ves errores de CORS, verifica que el backend tenga configurado:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:7190")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

### Error de conexión a API

Verifica que:
1. El backend esté corriendo en `http://localhost:5121`
2. La URL en `src/services/api.ts` sea correcta

## Próximas Mejoras

- [ ] Búsqueda de productos
- [ ] Filtros avanzados
- [ ] Eliminación de productos
- [ ] Exportación a Excel/PDF
- [ ] Validaciones más robustas
- [ ] Manejo de errores mejorado
- [ ] Tests unitarios
- [ ] Tests de integración
