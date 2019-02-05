import { Injectable } from '@angular/core';
import { Message } from '../models/message';
import { SnackBarComponent } from 'src/app/layout/material-components/snack-bar/snack-bar.component';

@Injectable()
export class MessageService {
    constructor(private snackBar: SnackBarComponent) {}

    showMessages(messages: Message[]) {
        messages.forEach((value, index) => {
            this.snackBar.openError(messages[0].text);
        })
    }
}