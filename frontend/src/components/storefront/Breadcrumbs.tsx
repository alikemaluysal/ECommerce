import { ChevronRight } from 'lucide-react';

interface BreadcrumbItem {
  label: string;
  onClick?: () => void;
}

interface BreadcrumbsProps {
  items: BreadcrumbItem[];
}

export default function Breadcrumbs({ items }: BreadcrumbsProps) {
  return (
    <nav aria-label="Breadcrumb" className="flex items-center gap-2 text-sm">
      {items.map((item, index) => (
        <div key={index} className="flex items-center gap-2">
          {index > 0 && <ChevronRight className="size-4 text-slate-400" />}
          {index === items.length - 1 ? (
            <span className="text-slate-900 font-semibold">{item.label}</span>
          ) : (
            <button 
              onClick={item.onClick}
              className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors"
            >
              {item.label}
            </button>
          )}
        </div>
      ))}
    </nav>
  );
}
