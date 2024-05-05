import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISucursalJc } from '../interface/ISucursalJc';
import { IRespuesta } from '../interface/IRespuesta';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SucursalQualaService {

  private endPoint: string = environment.endPoint;
  private apiURL: string = this.endPoint + "SucursalJcs/";

  constructor(private http: HttpClient) { }

  obtenerSucursales(): Observable<IRespuesta<ISucursalJc[]>> {
    return this.http.get<IRespuesta<ISucursalJc[]>>(`${this.apiURL}ObtenerSucursales`);
  }

  eliminarSucursal(id: number): Observable<IRespuesta<string>> {
    return this.http.delete<IRespuesta<string>>(`${this.apiURL}EliminarSucursal/${id}`);
  }

  actualizarSucursal(id: number, sucursal: ISucursalJc): Observable<any> {
    return this.http.put(`${this.apiURL}ActualizarSucursal/${id}`, sucursal);
  }

  crearSucursal(sucursal: ISucursalJc): Observable<IRespuesta<ISucursalJc>> {
    return this.http.post<IRespuesta<ISucursalJc>>(`${this.apiURL}CrearSucursal`, sucursal);
  }
}
