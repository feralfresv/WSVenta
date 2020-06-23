import { Component, OnInit } from '@angular/core';
import { ApiclienteService } from '../services/apicliente.service';
import { Response } from '../models/response';
import { DialogClienteComponent } from './dialog/dialogcliente.component';
import { MatDialog} from '@angular/material/dialog';


@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.scss']
})

export class ClienteComponent implements OnInit {

  public lst: any[];
  public columnas: string[] = ['id', 'nombre'];
  constructor(
    private apiCliente: ApiclienteService,
    public dialog: MatDialog
    )
    {}


  ngOnInit(): void {
    this.getClientes();
  }

  getClientes(){
    this.apiCliente.getClientes().subscribe(response =>{
        this.lst = response.data;
      });
  }

  openAdd(){
    const dialogRed = this.dialog.open(DialogClienteComponent, {
      width: '300'
    });
  }
}
