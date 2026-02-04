export default function Footer() {
  return (
    <footer className="bg-slate-50 border-t border-slate-200/60 mt-16">
      <div className="max-w-6xl mx-auto px-4 py-12">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          <div>
            <div className="text-xl font-semibold text-slate-900 mb-4">LUXE</div>
            <p className="text-sm text-slate-600">Premium products for modern living.</p>
          </div>

          <div>
            <div className="font-semibold text-slate-900 mb-4">Shop</div>
            <ul className="space-y-2 text-sm">
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">New Arrivals</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Best Sellers</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Sale</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Gift Cards</a></li>
            </ul>
          </div>

          <div>
            <div className="font-semibold text-slate-900 mb-4">Help</div>
            <ul className="space-y-2 text-sm">
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Contact Us</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Shipping & Returns</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">FAQ</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Size Guide</a></li>
            </ul>
          </div>

          <div>
            <div className="font-semibold text-slate-900 mb-4">Company</div>
            <ul className="space-y-2 text-sm">
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">About Us</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Careers</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Privacy Policy</a></li>
              <li><a href="#" className="text-slate-600 hover:text-indigo-600 hover:underline transition-colors">Terms of Service</a></li>
            </ul>
          </div>
        </div>

        <div className="border-t border-slate-200 mt-8 pt-8 text-center text-sm text-slate-600">
          © 2026 LUXE. All rights reserved.
        </div>
      </div>
    </footer>
  );
}
