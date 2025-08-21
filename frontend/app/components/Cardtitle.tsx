interface Props {
  brand: string;
  model: string;
}

export const CardTitle = ({ brand, model }: Props) => {
  return (
    <div
      style={{
        display: "flex",
        flexDirection: "row",
        alignItems: "center",
        justifyContent: "space-between",
      }}
    >
      <p className="card_brand">
        {brand} {model}
      </p>
    </div>
  );
};
