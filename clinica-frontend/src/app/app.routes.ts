import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { ListComponent } from './features/pacientes/list/list.component';
import { FormComponent } from './features/pacientes/form/form.component';
import { authGuard } from './core/guards/auth.guard';
import { DashboardComponent } from './features/dashboard/dashboard.component';

export const routes: Routes = [
    // Rota padrão (vazia) redireciona para login
    { path: '', redirectTo: 'login', pathMatch: 'full' },

    // Rota de login carrega o componente
    { path: 'login', component: LoginComponent },


    // Rota para o dashboard
    { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },

    // Rota para a lista de pacientes
    { path: 'pacientes', component: ListComponent, canActivate: [authGuard] },

    // Rota para o cadastro de pacientes
    { path: 'pacientes/novo', component: FormComponent, canActivate: [authGuard] }
];