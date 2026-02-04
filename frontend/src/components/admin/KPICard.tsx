import type { LucideIcon } from 'lucide-react';

interface KPICardProps {
  title: string;
  value: string;
  change?: string;
  icon: LucideIcon;
  trend?: 'up' | 'down';
}

export default function KPICard({ title, value, change, icon: Icon, trend }: KPICardProps) {
  return (
    <div className="bg-white border border-slate-200 rounded-xl p-6 shadow-sm hover:shadow-md transition-shadow">
      <div className="flex items-start justify-between mb-4">
        <div className="text-sm text-slate-600">{title}</div>
        <div className="bg-indigo-100 p-2 rounded-lg">
          <Icon className="size-5 text-indigo-600" />
        </div>
      </div>
      
      <div className="font-semibold text-slate-900 mb-2">{value}</div>
      
      {change && (
        <div className={`text-sm font-semibold ${
          trend === 'up' ? 'text-emerald-600' : trend === 'down' ? 'text-red-600' : 'text-slate-600'
        }`}>
          {change}
        </div>
      )}
    </div>
  );
}
