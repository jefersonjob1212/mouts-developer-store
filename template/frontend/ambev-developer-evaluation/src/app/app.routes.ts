import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./sales/pages/sales-home/sales-home.component').then(m => m.SalesHomeComponent)
    },
    {
        path: 'sales/create',
        loadComponent: () => import('./sales/pages/sale-create/sale-create.component').then(m => m.SaleCreateComponent)
    },
    {
        path: 'sales/edit/:id',
        loadComponent: () => import('./sales/pages/sale-create/sale-create.component').then(m => m.SaleCreateComponent)
    }
];
