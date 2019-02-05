import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Subscription } from 'rxjs';
import { MessageService } from '../shared/services';
declare var $: any;

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [
        trigger('scaleOut', [
            state('out', style({
                transform: 'scale(2)', opacity: 0
            })),
            transition('* => *', animate('200ms ease-out'))
          ])
    ]
})
export class LoginComponent implements OnInit {

    public form: FormGroup;
    public model: any = { username: '', password: '' };
    public loading = false;
    public currentState = '';
    subscription: Subscription;

    private finalState = 'out';

    constructor(
        private router: Router, 
        private formBuilder: FormBuilder, 
        private authenticationService: AuthenticationService,
        private messageService: MessageService) {}

    ngOnInit() {
        this.form = this.formBuilder.group({
            username: [
                '',
                [Validators.required, Validators.minLength(3)]
            ],
            password: [
                '',
                [Validators.required, Validators.minLength(4)]
            ]
        });
    }

    onSubmit(formValue: any) {
        this.loading = true;
        const username = this.form.get('username').value;
        const password = this.form.get('password').value;
        this.subscription = this.authenticationService.login(username, password)
        .subscribe(
            data => { this.onLoginSuccess(); },
            (response) => {
                this.loading = false;
                this.messageService.showMessages(response.error.messages);
            });
    }

    onLoginSuccess() {
        this.loading = false;
        this.currentState = this.finalState;
    }

    changeState() {
        this.currentState = this.currentState === 'initial' ? 'final' : 'initial';
    }

    onLoginFormDone(event: any) {
        if (event.toState !== this.finalState) { return; }
        localStorage.setItem('isLoggedin', 'true');
        this.router.navigate(['/dashboard']);
    }
}
