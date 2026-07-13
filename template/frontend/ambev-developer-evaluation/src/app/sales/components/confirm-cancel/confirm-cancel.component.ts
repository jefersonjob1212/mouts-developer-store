import { Component, inject, model } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-cancel',
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './confirm-cancel.component.html',
  styleUrl: './confirm-cancel.component.scss'
})
export class ConfirmCancelComponent {
  readonly dialogRef = inject(MatDialogRef<ConfirmCancelComponent>);
  readonly data = inject<string>(MAT_DIALOG_DATA);
  readonly cancel = model(this.data);
  
  onNoClick(): void {
    this.dialogRef.close();
  }
}
