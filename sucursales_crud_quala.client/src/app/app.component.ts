import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { OnInit } from '@angular/core';
import { ISucursalJc } from '../app/interface/ISucursalJc'
import { SucursalQualaService } from '../app/services/sucursal-quala-servicio.service'
import { IRespuesta } from './interface/IRespuesta';
import { MatDialog } from '@angular/material/dialog';
import { ModalSucursalQualaComponent } from './components/modal-sucursal-quala/modal-sucursal-quala.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogSucursalEliminarComponent } from '../app/components/dialog-sucursal-eliminar/dialog-sucursal-eliminar.component';



export interface UserData {
  id: string;
  name: string;
  progress: string;
  fruit: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = ['Identificacíon', 'Descripción', 'Dirección', 'Moneda', 'Acciones'];
  dataSource = new MatTableDataSource<ISucursalJc>();

  constructor
    (
    private sucursalQualaService: SucursalQualaService,
    public dialog: MatDialog,
    private _snackBar: MatSnackBar,
  ) {}

  ngOnInit(): void {
    this.obtenerSucursales();
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  openDialog() {
    this.dialog.open(ModalSucursalQualaComponent);
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  obtenerSucursales() {
    this.sucursalQualaService.obtenerSucursales().subscribe({
      next: (response) => {
        console.log(response);
        this.dataSource.data = response.dato!;
      },
      error: (error) => {
        console.error('Error al obtener sucursales:', error);
      }
    });
  }


  dialogoNuevoEmpleado() {
    this.dialog.open(ModalSucursalQualaComponent, {
      disableClose: true,
      width: "350px",
    }).afterClosed().subscribe(resultado => {
      if (resultado === "creado") {
        this.obtenerSucursales()
      }
    })
  }


  dialogoEditarEmpleado(dataSucursal: ISucursalJc) {
    this.dialog.open(ModalSucursalQualaComponent, {
      disableClose: true,
      width: "350px",
      data: dataSucursal,
    }).afterClosed().subscribe(resultado => {
      if (resultado === "editado") {
        this.obtenerSucursales()
      }
    })
  }


  mostrarAlerta(mensaje: string, accion: string) {
    this._snackBar.open(mensaje, accion, {
      horizontalPosition: "end",
      verticalPosition: "top",
      duration: 3000
    });
  }

  dialogoEliminarSucursal(dataSucursal: ISucursalJc) {
    this.dialog.open(DialogSucursalEliminarComponent, {
      disableClose: true,
      data: dataSucursal,
    }).afterClosed().subscribe(resultado => {
      if (resultado === "eliminar") {
        this.sucursalQualaService.eliminarSucursal(dataSucursal.codigo).subscribe({
            next: (data) => {
            this.mostrarAlerta(data.mensaje!, "error")
            this.obtenerSucursales();
            },
            error: (data) => {
              this.mostrarAlerta(data.mensaje!, "error")
            }
          });
      }
    })

  }
}



