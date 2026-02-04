import { Star } from 'lucide-react';

interface RatingProps {
  rating?: number;
  reviewCount?: number;
}

export default function Rating({ rating = 4.5, reviewCount = 128 }: RatingProps) {
  return (
    <div className="flex items-center gap-2">
      <div className="flex items-center gap-1">
        {[1, 2, 3, 4, 5].map((star) => (
          <Star 
            key={star} 
            className={`size-4 ${
              star <= rating 
                ? 'fill-amber-400 text-amber-400' 
                : 'fill-slate-200 text-slate-200'
            }`}
          />
        ))}
      </div>
      <span className="text-sm text-slate-600">
        {rating} ({reviewCount} reviews)
      </span>
    </div>
  );
}
