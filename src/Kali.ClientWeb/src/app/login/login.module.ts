import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule, MatCheckboxModule, MatInputModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Config } from '../shared/services/config.service';
import { MaterialComponentsModule } from '../layout/material-components/material-components.module';
import { SnackBarComponent } from '../layout/material-components/snack-bar/snack-bar.component';
import { MessageService, CryptographyService } from '../shared/services';

@NgModule({
    imports: [
        CommonModule,
        LoginRoutingModule,
        MatInputModule,
        MatCheckboxModule,
        MatButtonModule,
        FlexLayoutModule.withConfig({addFlexToParent: false}),
        FormsModule,
        ReactiveFormsModule,
        MatProgressSpinnerModule,
        MaterialComponentsModule
    ],
    declarations: [LoginComponent],
    providers: [AuthenticationService, Config, MessageService, SnackBarComponent, CryptographyService]
})
export class LoginModule {}
