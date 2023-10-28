'use client';

import { GroupBase, OnChangeValue, SingleValue, StylesConfig } from "react-select";
import { useMounted } from "./useMounted";
import AsyncSelect from "react-select/async";
import { useState } from "react";


const styles: StylesConfig<MyOption, false, GroupBase<MyOption>> = {
  control: (baseStyles, state) => ({
    ...baseStyles,
    borderColor: state.isFocused ? "#FFA4A3" : "gray",
    boxShadow: state.isFocused ? "0 0 0 1px #FFA4A3" : "",
    "&:hover": {
      borderColor: "#FFA4A3",
    },
  }),
  option: (baseStyles, state) => ({
    ...baseStyles,
    color: state.isSelected ? "#472E2E" : "",
    backgroundColor: state.isSelected ? "#FFA4A3" : "",
    "&:hover": {
      backgroundColor: !state.isSelected ? "#FFF6F6" : "",
    },
  }),
};

interface MyOption {
  label: string;
  value: string;
}

export default function Home() {
  const { hasMounted } = useMounted();
  const [selected, setSelected] = useState<MyOption>();

  async function fetctApi(value: string): Promise<MyOption[]> {
    const resp = await fetch("https://rickandmortyapi.com/api/character");
    const json = await resp.json();
    const characterList = json.results;
    const options: MyOption[] = characterList.map((item: { id: string; name: string }) => ({
      value: item.id + "",
      label: item.name,
    }));

    return options.filter((i: MyOption) =>
      i.label.toLowerCase().includes(value.toLowerCase()))
  }

  return (
    <main style={{
      width: "320px"
    }}>
      <h1>React Select</h1>
      {
        hasMounted ?
          <AsyncSelect
            styles={styles}
            defaultOptions={true}
            loadOptions={fetctApi}
            cacheOptions={true}
            loadingMessage={() => "Carregando"}
            onChange={(data) => setSelected(data as MyOption)}
          /> : null
      }
      {selected && <pre>{JSON.stringify(selected)}</pre>}
    </main>
  )
}
