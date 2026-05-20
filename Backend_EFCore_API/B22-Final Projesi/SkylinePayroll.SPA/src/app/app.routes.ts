import { Routes } from '@angular/router';
import { Dashboard } from './features/dashboard/dashboard';
import { Payroll } from './features/payroll/payroll';
import { Login } from './components/login/login';
import { Landing } from './components/landing/landing';
import { Register } from './components/register/register';
import { ForgotPassword } from './components/forgot-password/forgot-password';
import { ResetPassword } from './components/reset-password/reset-password';
import { Management } from './components/management/management';
import { DepartmentRoleManagement } from './components/management/department-role-management/department-role-management';
import { HrPanelComponent } from './components/hr-panel/hr-panel.component';
import { AccountingPayout } from './components/accounting-payout/accounting-payout';
import { EmployeeTerminationPanel } from './components/employee-termination-panel/employee-termination-panel';
export const routes: Routes = [
  { path: '', redirectTo: 'landing', pathMatch: 'full' },
  { path: 'landing', component: Landing },
  { path: 'register', component: Register },
  { path: 'dashboard', component: Dashboard },
  { path: 'payroll', component: Payroll },
  { path: 'login', component: Login },
  { path: 'forgot-password', component: ForgotPassword },
  { path: 'reset-password', component: ResetPassword },
  { path: 'management', component: Management },
  { path: 'department-role-management', component: DepartmentRoleManagement },
  { path: 'hr-panel', component: HrPanelComponent },
  { path: 'payout', component: AccountingPayout },
  { path: 'employee-panel', component: EmployeeTerminationPanel },
];
