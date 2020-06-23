import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiclienteService } from 'src/app/services/apicliente.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Cliente } from 'src/app/models/cliente';


@Component({
    templateUrl: 'dialogcliente.component.html'
})

export class DialogClienteComponent {
    public nombre: string;

    constructor(
        public dialogRef: MatDialogRef<DialogClienteComponent>,
        public apiCliente: ApiclienteService,
        public snackBar: MatSnackBar,
        ) {}

    close(){
        this.dialogRef.close();
    }

    addCliente(){
        console.log('addcliente()');
        const cliente: Cliente = {nombre: this.nombre};
        this.apiCliente.add(cliente).subscribe(response => {
            if (response.exito === 1){
                this.dialogRef.close();
                this.snackBar.open('Cliente insertado con éxito', '', {
                    duration: 2000
                });
            }
        });
    }
}

