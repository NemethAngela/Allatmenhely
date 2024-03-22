import { Component, Inject } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { LoginRequestModel } from "src/app/models/loginrequestmodel.model";
import { Router } from '@angular/router';
import { AdminService } from "src/app/services/admin.service";

@Component({
  selector: 'logindialog',
  templateUrl: './logindialog.html',
})
export class LoginDialog {
  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]);
  hide = true;

  constructor(
    private router: Router,
    public dialogRef: MatDialogRef<LoginDialog>,
    private adminService: AdminService
  ) {
    // Csak a tesztelés megkönnyítése miatt
    this.email.setValue("nemethangela75@gmail.com");
    this.password.setValue("almafa");
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  getErrorMessage() {
    if (this.email.hasError('required')) {
      return 'Az email cím megadása kötelező';
    }

    return this.email.hasError('email') ? 'Az email cím hibás' : '';
  }

  onLoginClick($event: MouseEvent) {
    
    if (this.email.valid && this.password.valid) {
      const loginRequest: LoginRequestModel = {
        email: this.email.value !== null ? this.email.value : "",
        password: this.password.value !== null ? this.password.value : ""
      };
  
      this.adminService.login(loginRequest).subscribe(
        loginResponse => {
          if (!loginResponse.isError){
            console.log('Sikeres bejelentkezés', loginResponse.email);
            console.log('Token', loginResponse.token);
            this.dialogRef.close({ loginResponse });
            this.router.navigate(['../../allatlista']);
          }
          else{
            console.log('Bejelentkezés sikertelen', loginResponse.errorMessage);
          }
        },
        error => {
          console.log('Bejelentkezés sikertelen', error);
        }
      );

    } else {
        console.log("Az email vagy a jelszó érvénytelen!");
    }
  }
}
