"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import {
  Package,
  ShoppingCart,
  ShoppingBag,
  Warehouse,
  Users,
  BarChart3,
  Settings,
  Store,
} from "lucide-react";
import { cn } from "@/lib/utils";
import { Separator } from "@/components/ui/separator";

const navigation = [
  {
    name: "Dashboard",
    href: "/",
    icon: BarChart3,
  },
  {
    name: "Productos",
    href: "/products",
    icon: Package,
  },
  {
    name: "Ventas",
    href: "/sales",
    icon: ShoppingCart,
  },
  {
    name: "Compras",
    href: "/purchases",
    icon: ShoppingBag,
  },
  {
    name: "Almacenes",
    href: "/warehouses",
    icon: Warehouse,
  },
  {
    name: "Clientes/Proveedores",
    href: "/persons",
    icon: Users,
  },
  {
    name: "Categorías",
    href: "/categories",
    icon: Store,
  },
];

export function Sidebar() {
  const pathname = usePathname();

  return (
    <div className="flex h-full w-64 flex-col border-r border-border bg-card">
      {/* Header */}
      <div className="p-6">
        <h1 className="text-2xl font-bold text-primary">POS System</h1>
        <p className="text-sm text-muted-foreground">Gestión de Inventario</p>
      </div>

      <Separator />

      {/* Navigation */}
      <nav className="flex-1 space-y-1 p-4">
        {navigation.map((item) => {
          const isActive = pathname === item.href;
          return (
            <Link
              key={item.name}
              href={item.href}
              className={cn(
                "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
                isActive
                  ? "bg-primary text-primary-foreground"
                  : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
              )}
            >
              <item.icon className="h-5 w-5" />
              {item.name}
            </Link>
          );
        })}
      </nav>

      <Separator />

      {/* Footer */}
      <div className="p-4">
        <Link
          href="/settings"
          className={cn(
            "flex items-center gap-3 rounded-lg px-3 py-2 text-sm font-medium transition-colors",
            pathname === "/settings"
              ? "bg-primary text-primary-foreground"
              : "text-muted-foreground hover:bg-accent hover:text-accent-foreground"
          )}
        >
          <Settings className="h-5 w-5" />
          Configuración
        </Link>
      </div>
    </div>
  );
}
