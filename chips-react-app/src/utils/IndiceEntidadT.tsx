import axios, { AxiosResponse } from "axios";
import { ReactElement, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Paginacion from "./Paginacion";
import confirmar from "./Confirmar";
import { Button } from "./Buttons";
import { ListadoGenerico } from "./ListadoGenerico";

const selectStyle = {
  textDecoration: "underline",
  color: "blue",
  fontWeight: "normal",
};


//import '../App.css';

export const IndiceEntidadT = <T extends {}>(props: indiceEntidadTProps<T>) => {
  const [entidades, setEntidades] = useState<T[]>();
  const [totalDePaginas, setTotalDePaginas] = useState(0);
  const [recordsPorPagina, setRecordsPorPagina] = useState(10);
  const [pagina, setPagina] = useState(1);

  useEffect(() => {
    axios
      .get(props.url, {
        params: { pagina, recordsPorPagina },
      })
      .then((respuesta: AxiosResponse<T[]>) => {
        //Conectar la paginación
        const totalDeRegistros = parseInt(
          respuesta.headers["cantidadtotalregistros"],
          10
        );
        setTotalDePaginas(Math.ceil(totalDeRegistros / recordsPorPagina));
        console.log("USE EFFECT", respuesta.data);
        setEntidades(respuesta.data);
        //console.log('entidades',entidades);
      });
  }, [pagina, recordsPorPagina]);

  function cargarDatos() {
    axios
      .get(props.url, {
        params: { pagina, recordsPorPagina },
      })
      .then((respuesta: AxiosResponse<T[]>) => {
        const totalDeRegistros = parseInt(
          respuesta.headers["cantidadtotalregistros"],
          10
        );
        setTotalDePaginas(Math.ceil(totalDeRegistros / recordsPorPagina));
        setEntidades(respuesta.data);
      });
  }

  async function borrar(id: number) {
    try {
      await axios.delete(`${props.url}/${id}`);
      cargarDatos();
    } catch (error) {
      console.log(error);
    }
  }

  const botones = (urlEditar: string, id: number) => (
    <>
      {/* <Link className="btn btn-success" to={urlEditar}>
        Editar
      </Link> */}
      <Button
        onClick={() => confirmar(() => borrar(id))}
        className="btn btn-danger"
      >
        Borrar
      </Button>
    </>
  );

  return (
    <>
      <div className="container">
        <h3>{props.titulo}</h3>
        <code>
          <div>
          <label>Lineas por página</label>
            <select
              className="form-select form-select-sm form-control"
              style={{ width: "80px" }}
              aria-label="Default select example"
              defaultValue={10}
              onChange={(e) => {
                setPagina(1);
                setRecordsPorPagina(parseInt(e.currentTarget.value, 10));
              }}
            >
              <option value={10}>10</option>
              <option value={20}>20</option>
              <option value={30}>30</option>
            </select>
          </div>
        </code>

        <ListadoGenerico listado={entidades}>
          <table className="table table-striped">
            {props.children(entidades!, botones)}
          </table>
        </ListadoGenerico>

        <code>
        <tfoot className="table-light">          
        <Paginacion
          cantidadTotalDePaginas={totalDePaginas}
          paginaActual={pagina}
          onChange={(nuevaPagina) => setPagina(nuevaPagina)}
          />
        </tfoot>
        </code>      
      </div>
    </>
  );
};

interface indiceEntidadTProps<T> {
  url: string;
  urlCrear?: string;
  children(
    entidades: T[],
    botones: (urlEditar: string, id: number) => ReactElement
  ): ReactElement;
  titulo: string;
  nombreEntidad?: string;
}
export default IndiceEntidadT;
