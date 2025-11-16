# POS System - Frontend

Sistema de Punto de Venta y Gestión de Inventario construido con Next.js 14, TypeScript y shadcn/ui.

## Características

- **Dashboard** con métricas de ventas, compras e inventario
- **Gestión de Productos** con CRUD completo
- **Sidebar de navegación** responsive
- **Componentes UI** modernos con shadcn/ui
- **Integración con API** backend .NET
- **TypeScript** para seguridad de tipos
- **Tailwind CSS v4** para estilos

## Tecnologías

- [Next.js 14](https://nextjs.org/) - Framework React
- [TypeScript](https://www.typescriptlang.org/) - Tipado estático
- [Tailwind CSS v4](https://tailwindcss.com/) - Framework CSS
- [shadcn/ui](https://ui.shadcn.com/) - Componentes UI
- [React Hook Form](https://react-hook-form.com/) - Manejo de formularios
- [Zod](https://zod.dev/) - Validación de esquemas
- [Lucide React](https://lucide.dev/) - Iconos

## Configuración Inicial

1. **Instalar dependencias:**

```bash
npm install
```

2. **Configurar variables de entorno:**

Crea un archivo `.env.local` en la raíz del proyecto:

```env
NEXT_PUBLIC_API_URL=https://localhost:7190
```

Ajusta la URL del backend según tu configuración.

3. **Ejecutar el servidor de desarrollo:**

```bash
npm run dev
```

Abre [http://localhost:3000](http://localhost:3000) en tu navegador.

## Estructura del Proyecto

```
frontend/
├── app/                    # App Router de Next.js
│   ├── layout.tsx         # Layout principal
│   ├── page.tsx           # Dashboard
│   └── products/          # Módulo de productos
├── components/            # Componentes React
│   ├── layout/           # Componentes de layout
│   ├── products/         # Componentes de productos
│   └── ui/              # Componentes UI de shadcn
├── lib/                  # Utilidades y configuración
│   ├── api/             # Cliente API
│   └── utils.ts         # Funciones auxiliares
└── types/               # Definiciones TypeScript
```

## Módulos Implementados

### Productos

- Lista de productos con búsqueda
- Crear nuevo producto
- Editar producto existente
- Validación de formularios
- Integración con categorías, unidades de medida y tipos de IGV

## Próximos Módulos

- Ventas
- Compras
- Almacenes
- Clientes/Proveedores
- Categorías
- Inventario

## Scripts Disponibles

```bash
# Desarrollo
npm run dev

# Producción
npm run build
npm run start

# Linting
npm run lint
```

## Integración con Backend

El frontend se conecta al backend .NET a través de la API REST. Asegúrate de que:

1. El backend esté ejecutándose en `https://localhost:7190` (o la URL configurada)
2. CORS esté configurado correctamente en el backend
3. Los endpoints de la API estén disponibles

## Notas de Desarrollo

- El proyecto usa **Tailwind CSS v4** con la sintaxis `@theme inline`
- Los colores y variables de tema están configurados en `app/globals.css`
- Los componentes UI siguen el patrón de shadcn/ui con variantes personalizables
- La navegación usa el App Router de Next.js 14
