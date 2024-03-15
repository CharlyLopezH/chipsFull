import { useState } from "react";
import { prestadoresEndpoint } from "../utils/endpoints";
import IndiceEntidadT from "../utils/IndiceEntidadT";
import { PrestadorDTO } from "../models/prestador.model";
import { LuArrowUpWideNarrow, LuArrowDownWideNarrow } from "react-icons/lu";

export const IndicePrestadores = () => {
  const [sortBy, setSortBy] = useState<{
    key: string;
    ascending: boolean;
  } | null>({ key: "primerAp", ascending: false });
  const onHandleSort = (key: string) => {
    if (sortBy && sortBy.key === key) {
      console.log("ordenaré por " + key);
      //console.log('Estoy en el if del sortBy-->'+'sortBy= '+ sortBy+' sortBy.key= '+key);
      setSortBy({ ...sortBy, ascending: !sortBy.ascending });
    } else {
      setSortBy({ key, ascending: true });
    }
  };

  //Render Icon
  const renderIcon = (sortByKey: string, ascending: any) => {
    if (sortBy && sortBy.key === sortByKey) {
      return ascending ? <LuArrowUpWideNarrow /> : <LuArrowDownWideNarrow />;
    }
    return null;
  };

  //Styles
  const ulStyle = {
    textDecoration: "underline",
    color: "blue",
    fontWeight: "normal",
  };

  return (
    <>
      <IndiceEntidadT<PrestadorDTO>
        url={prestadoresEndpoint}
        urlCrear="generos/crear"
        titulo="PSP's"
        nombreEntidad="Prestadores"
      >
        {(prests, botones) => (
          <>
            <thead>
              <tr>
                <th style={ulStyle} onClick={() => onHandleSort("id")}>
                  Id {renderIcon("id", sortBy?.ascending)}
                </th>

                <th style={ulStyle} onClick={() => onHandleSort("nombres")}>
                  Nombre {renderIcon("nombres", sortBy?.ascending)}
                </th>
                <th style={ulStyle} onClick={() => onHandleSort("primerAp")}>
                  <u>Apellidos</u> {renderIcon("primerAp", sortBy?.ascending)}
                </th>
                <th style={ulStyle} onClick={() => onHandleSort("email")}>
                  Email {renderIcon("email", sortBy?.ascending)}
                </th>
                {/* <th>Acciones </th> */}
              </tr>
            </thead>
            <tbody className="table-group-divider">
              {prests
                ?.sort((a, b) => {
                  if (!sortBy) return 0;
                  const order = sortBy.ascending ? 1 : -1;
                  if (a[sortBy.key] < b[sortBy.key]) return -1 * order;
                  if (a[sortBy.key] > b[sortBy.key]) return 1 * order;
                  return 0;
                })
                .map((item) => (
                  <tr
                    style={{
                      backgroundColor: "lightblue",
                      fontSize: "14px",
                      paddingTop: "0rem",
                      paddingBottom: "0rem",
                    }}
                    key={item.id}
                  >
                    <td>{item.id}</td>
                    <td>{item.nombres}</td>

                    <td>
                      {item.primerAp + " " + item.segundoAp}
                      {/* {item.apellidos} */}
                    </td>

                    <td>{item.email}</td>
                    {/* 
                                <td>
                                    {botones(`prestadores/borrar/${item.id}`, item.id)}                                    
                                </td> */}
                  </tr>
                ))}
            </tbody>

          </>
        )}
      </IndiceEntidadT>
    </>
  );
};

export default IndicePrestadores;
