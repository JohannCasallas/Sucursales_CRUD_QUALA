import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ISucursalJc } from '../../interface/ISucursalJc';

@Component({
  selector: 'app-dialog-sucursal-eliminar',
  templateUrl: './dialog-sucursal-eliminar.component.html',
  styleUrls: ['./dialog-sucursal-eliminar.component.css']
})
export class DialogSucursalEliminarComponent implements OnInit {

  constructor(
    private dialogoReferencia: MatDialogRef<DialogSucursalEliminarComponent>,
    @Inject(MAT_DIALOG_DATA) public dataSucursal: ISucursalJc
  ) { }

  ngOnInit(): void {
  }

  validarEliminar() {
    if (this.dataSucursal) {
      this.dialogoReferencia.close("eliminar")
    }
  }

}
