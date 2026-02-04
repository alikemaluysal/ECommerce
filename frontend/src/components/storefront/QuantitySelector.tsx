import { Minus, Plus } from 'lucide-react';

interface QuantitySelectorProps {
  quantity?: number;
  onIncrease?: () => void;
  onDecrease?: () => void;
}

export default function QuantitySelector({ quantity = 1, onIncrease, onDecrease }: QuantitySelectorProps) {
  return (
    <div className="inline-flex items-center border border-slate-200 rounded-lg overflow-hidden">
      <button 
        onClick={onDecrease}
        disabled={quantity <= 1}
        className="px-3 py-2 hover:bg-slate-50 transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500 disabled:opacity-50 disabled:cursor-not-allowed"
        aria-label="Decrease quantity"
      >
        <Minus className="size-4 text-slate-600" />
      </button>
      <div className="px-4 py-2 border-x border-slate-200 min-w-[3rem] text-center font-semibold text-slate-900">
        {quantity}
      </div>
      <button 
        onClick={onIncrease}
        className="px-3 py-2 hover:bg-slate-50 transition-colors focus:outline-none focus:ring-2 focus:ring-indigo-500"
        aria-label="Increase quantity"
      >
        <Plus className="size-4 text-slate-600" />
      </button>
    </div>
  );
}
