'use client';

import { GroupBase, OnChangeValue, SingleValue, StylesConfig } from "react-select";
import { useMounted } from "./useMounted";
import AsyncSelect from "react-select/async";
import { useState } from "react";
import { Controller, useForm } from "react-hook-form";


const styles: StylesConfig<MyOption, false, GroupBase<MyOption>> = {
  control: (baseStyles, state) => ({
    ...baseStyles,
    borderColor: state.isFocused ? "#FFA4A3" : baseStyles.borderColor,
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

interface Input {
  characterId: number;
}

export default function Home() {
  const { hasMounted } = useMounted();
  const [selected, setSelected] = useState<MyOption>();
  const { register, handleSubmit, control, watch } = useForm<Input>();

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

  function submit(data: Input) {
    console.log(data);
  }

  return (
    <main style={{
      width: "320px"
    }}>
      <h1>React Select</h1>
      <form onSubmit={handleSubmit(submit)}>
        {
          hasMounted ? <Controller
            name="characterId"
            control={control}
            render={({ field }) => (
              <AsyncSelect
                {...field}
                styles={styles}
                defaultOptions={true}
                loadOptions={fetctApi}
                cacheOptions={true}
                loadingMessage={() => "Carregando"}
                value={selected}
                onChange={(data) => {
                  setSelected(data as MyOption);
                  field.onChange(data?.value);
                }}
              />

            )}
          />
            : null
        }
        <input type="submit" value="Submit"/>
      </form>
      {selected && <pre>{JSON.stringify(selected)}</pre>}
    </main>
  )
}
