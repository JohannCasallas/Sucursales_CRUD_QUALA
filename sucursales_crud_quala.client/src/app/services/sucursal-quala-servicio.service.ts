import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISucursalJc } from '../interface/ISucursalJc';
import { IRespuesta } from '../interface/IRespuesta';


@Injectable({
  providedIn: 'root'
})
export class SucursalQualaServicioService {

  private apiURL: string = ''; 

  constructor(private http: HttpClient) { }

  obtenerSucursales(): Observable<IRespuesta<ISucursalJc[]>> {
    return this.http.get<IRespuesta<ISucursalJc[]>>(`${this.apiURL}/api/SucursalJcs/ObtenerSucursales`);
  }

  eliminarSucursal(id: number): Observable<IRespuesta<string>> {
    return this.http.delete<IRespuesta<string>>(`${this.apiURL}/api/SucursalJcs/EliminarSucursal/${id}`);
  }

  actualizarSucursal(id: number, sucursal: ISucursalJc): Observable<any> {
    return this.http.put(`${this.apiURL}/api/SucursalJcs/ActualizarSucursal/${id}`, sucursal);
  }

  crearSucursal(sucursal: ISucursalJc): Observable<IRespuesta<ISucursalJc>> {
    return this.http.post<IRespuesta<ISucursalJc>>(`${this.apiURL}/api/SucursalJcs/CrearSucursal`, sucursal);
  }
}
