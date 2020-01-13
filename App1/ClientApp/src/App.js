import React, { useState, useEffect } from "react";
import CircularProgress from "@material-ui/core/CircularProgress";
import Checkout from "./Checkout";

function App() {
  var [check, setCheck] = useState({ auth: false, loading: true });

  const checkAuth = async () => {
    const url = window.location.href;
    const urlArray = url.split("token=");
    if (urlArray.length == 2) {
      const token = urlArray[1];
      const authUrl = `/Auth?token=${token}`;
      fetch(authUrl)
        .then(function(data) {
          if (data.status == 200) setCheck({ auth: true, loading: false });
          else setCheck({ ...check, loading: false });
        })
        .catch(function(error) {
          setCheck({ ...check, loading: false });
        });
    } else {
      setCheck({ ...check, loading: false });
    }
  };

  useEffect(() => {
    checkAuth();
  }, []);

  return (
    <>
      {check.auth ? (
        <Checkout />
      ) : (
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "100vh",
            backgroundColor: "grey"
          }}
        >
          {check.loading ? (
            <CircularProgress />
          ) : (
            "Sem permissão para acessar !"
          )}
        </div>
      )}
    </>
  );
}

export default App;
