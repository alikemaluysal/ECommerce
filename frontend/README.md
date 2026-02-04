# E-Commerce React Application

Modern bir e-ticaret uygulaması - React, TypeScript, Vite ve Tailwind CSS ile geliştirilmiştir.

## Özellikler

- ✅ Modern React 19 + TypeScript
- ✅ Vite build tool
- ✅ Tailwind CSS styling
- ✅ Backend API entegrasyonu
- ✅ Otomatik token yönetimi
- ✅ Type-safe API client
- ✅ Toast bildirimler
- ✅ Admin paneli
- ✅ Sepet yönetimi (Guest & Auth)

## Kurulum

1. Bağımlılıkları yükleyin:
```bash
npm install
```

2. Environment variables ayarlayın:
```bash
# .env dosyası oluşturun
cp .env.example .env

# Backend URL'ini ayarlayın
VITE_API_BASE_URL=http://localhost:5278/api
```

3. Development server'ı başlatın:
```bash
npm run dev
```

## Backend API Entegrasyonu

Backend API'yi kullanmak için detaylı dökümantasyon: [API_USAGE.md](./API_USAGE.md)

### Hızlı Başlangıç

```tsx
import { useAuth } from './context/AuthContext';
import { productsApi } from './api';

// Auth kullanımı
const { login, user } = useAuth();
await login('admin@alikemaluysal.com', '112');

// API kullanımı
const products = await productsApi.getProducts(0, 10);
```

## Proje Yapısı

```
src/
├── api/                    # Backend API entegrasyonu
│   ├── client.ts          # Axios instance ve interceptor'lar
│   ├── index.ts           # Merkezi export
│   └── endpoints/         # API endpoint fonksiyonları
├── components/
│   ├── admin/             # Admin panel bileşenleri
│   ├── storefront/        # E-ticaret sayfaları
│   └── ui/                # Reusable UI bileşenleri
├── context/               # React Context'ler
├── types/                 # TypeScript type tanımları
├── utils/                 # Utility fonksiyonlar
└── data/                  # Mock data (development)
```

## Admin Panel

Admin paneline erişim:
- URL: `/admin/login`
- Email: `admin@alikemaluysal.com`
- Password: `112`

## Scripts

```bash
npm run dev      # Development server
npm run build    # Production build
npm run preview  # Preview production build
npm run lint     # ESLint kontrolü
```

## Teknolojiler

- React 19
- TypeScript
- Vite
- Tailwind CSS
- Axios
- Sonner (Toast notifications)
- Lucide React (Icons)
- React Router DOM
import reactDom from 'eslint-plugin-react-dom'

export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...
      // Enable lint rules for React
      reactX.configs['recommended-typescript'],
      // Enable lint rules for React DOM
      reactDom.configs.recommended,
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```
