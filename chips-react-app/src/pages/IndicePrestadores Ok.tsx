import axios, { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { ListadoGenerico } from "../utils/ListadoGenerico";
import { PrestadorDTO } from "../models/prestador.model";
import { prestadoresEndpoint } from "../utils/endpoints";
import Paginacion from "../utils/Paginacion";


export const IndicePrestadores=()=>{

    //console.log(urlIndiceRuspejs);    

const [prestadorData, setprestadorData]=useState<PrestadorDTO[]>();
const[totalDePaginas, setTotalDePaginas]=useState(0);
const [recordsPorPagina,setRecordsPorPagina] = useState(15);
const [pagina,setPagina] =useState(1);
const handleOnMouseOver=()=>{
    console.log('Hola, Mouse over field je je');
}
    
useEffect(()=>{
    console.log(prestadoresEndpoint);
    axios.get(prestadoresEndpoint, {
        //params:{pagina, recordsPorPagina}
    })
    .then((respuesta:AxiosResponse<PrestadorDTO[]>)=>{        
        //Conectar la paginación
        const totalDeRegistros = parseInt(respuesta.headers['cantidadtotalregistros'],10);
        setTotalDePaginas(Math.ceil(totalDeRegistros/recordsPorPagina));
          console.log(respuesta.data);   
          //Respuesta data será enviado a Listado Genérico 
          setprestadorData(respuesta.data);      
    })

},[pagina, recordsPorPagina])

    return(
        <>
        <div className="container">         
        <h3>Registro de Servidores Públicos (RUSPEJ)</h3>

        <div className="form-group" style={{width:'150px'}} > 
        <label>Filas por página</label>
        <select 
            className="form-control"
            defaultValue={10}
            onChange={e=>{
            setPagina(1);
            setRecordsPorPagina(parseInt(e.currentTarget.value,10))}}>
            <option value={10}>10</option>
            <option value={20}>20</option>
            <option value={30}>30</option>
        </select>
        </div>

        <Paginacion 
            cantidadTotalDePaginas={totalDePaginas}
            paginaActual={pagina}
            onChange={nuevaPagina=>setPagina(nuevaPagina)}
        />


        <ListadoGenerico listado={prestadorData} />
        <table className="table table-light table-striped table-hover custom-table ">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Nombres</th>
                    <th>Apellidos</th>
                    <th>email</th>
                </tr>
            </thead>
            <tbody className="left-align-text">
                {prestadorData?.map(i=>
                    <tr key={i.id}>
                     <td>{i.id}</td>
                     <td>{i.nombres}</td>
                     <td>{i.primerAp}</td>
                     <td>{i.email}</td>  
                    </tr>
                    )}
            </tbody>
        </table>

        </div>
        </>
    )
}

export default IndicePrestadores;