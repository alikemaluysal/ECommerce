import type { OrderStatus } from '../../types';

interface StatusBadgeProps {
  status: OrderStatus;
}

export default function StatusBadge({ status }: StatusBadgeProps) {
  const styles: Record<OrderStatus, string> = {
    Received: 'bg-blue-100 text-blue-700',
    Preparing: 'bg-amber-100 text-amber-700',
    Shipped: 'bg-indigo-100 text-indigo-700',
    Delivered: 'bg-emerald-100 text-emerald-700',
    Cancelled: 'bg-slate-100 text-slate-700',
  };

  return (
    <span className={`inline-flex items-center px-2.5 py-1 rounded-lg text-xs font-semibold ${styles[status]}`}>
      {status}
    </span>
  );
}
