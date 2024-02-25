import React from "react";

function useFetch<E>() {
    const [data, setData] = React.useState<E | null>(null);
    const [error, setError] = React.useState<string | null>(null);
    const [loading, setLoading] = React.useState<boolean>(false);

    async function request(url: string, options?: RequestInit) {
        let resp, json;
        try {
            setError(null);
            setLoading(true);
            resp = await fetch(url, options);
            json = await resp.json();
        } catch (error) {
            json = null;
            setError('Falha ao realizar a requisição');
        } finally {
            setLoading(false);
            setData(json);
            return { resp, json };
        }
    }

    return { data, error, loading, request }
}

export default useFetch;