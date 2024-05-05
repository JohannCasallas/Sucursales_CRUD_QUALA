import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IMonedaJc } from '../../interface/IMonedaJc';
import { SucursalQualaService } from '../../services/sucursal-quala-servicio.service';
import { MonedaServicio } from '../../services/moneda-servicio';
import { ISucursalJc } from '../../interface/ISucursalJc';

@Component({
  selector: 'app-modal-sucursal-quala',
  templateUrl: './modal-sucursal-quala.component.html',
  styleUrls: ['./modal-sucursal-quala.component.css']
})
export class ModalSucursalQualaComponent implements OnInit {
  FormSucursal: FormGroup;
  tituloAccion: string = "Agregar Nueva";
  botonAccion: string = "Agregar";
  ListaMonedas: IMonedaJc[] = [];

  constructor(
    private dialogoReferencia: MatDialogRef<ModalSucursalQualaComponent>,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _sucursalQualaService: SucursalQualaService,
    private _monedaServicio: MonedaServicio,
    @Inject(MAT_DIALOG_DATA) public dataSucursal: ISucursalJc
  ) {
    this.FormSucursal = this.fb.group({
      identificacion: ['', Validators.required],
      descripcion: ['', Validators.required],
      direccion: ['', Validators.required],
      monedaId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.obtenerMonedas()
    if (this.dataSucursal) {
      this.FormSucursal.patchValue({
        identificacion: this.dataSucursal.identificacion,
        descripcion: this.dataSucursal.descripcion,
        direccion: this.dataSucursal.direccion,
        monedaId: this.dataSucursal.monedaId,
      })

      this.tituloAccion = "Editar"
      this.botonAccion = "Actualizar"
    }
  }

  obtenerMonedas() {
    this._monedaServicio.obtenerMonedas().subscribe({
      next: (data) => {

        this.ListaMonedas = data.dato || [];
      },
      error: (data) => {
        this.mostrarAlerta(data.mensaje, "error")
      }
    });
  }

  crearActualizarSucursal() {
    const modeloSucursal: ISucursalJc = {
      codigo: this.dataSucursal.codigo,
      identificacion: this.FormSucursal.value.identificacion,
      descripcion: this.FormSucursal.value.descripcion,
      direccion: this.FormSucursal.value.direccion,
      monedaId: this.FormSucursal.value.monedaId,
    }

    if (this.dataSucursal == null) {
      this._sucursalQualaService.crearSucursal(modeloSucursal).subscribe({
        next: (data) => {
          this.mostrarAlerta(data.mensaje!, "Exito")
          this.dialogoReferencia.close("creado");
        },
        error: (data) => {
          this.mostrarAlerta(data.mensaje!, "error")
        }
      })
    } else {
      this._sucursalQualaService.actualizarSucursal(this.dataSucursal.codigo, modeloSucursal).subscribe({
        next: (data) => {
          this.mostrarAlerta(data.mensaje!, "Exito")
          this.dialogoReferencia.close("editado");
        },
        error: (data) => {
          this.mostrarAlerta(data.mensaje!, "error")
        }
      })
    }
  }

  mostrarAlerta(mensaje: string, accion: string) {
    this._snackBar.open(mensaje, accion, {
      horizontalPosition: "end",
      verticalPosition: "top",
      duration: 3000
    });
  }

}
