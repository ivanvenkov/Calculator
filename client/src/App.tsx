import {
  createTheme,
  CssBaseline,
  ThemeProvider,
} from "@mui/material";
import Calculator from "./app/Calculator";

function App() {
  const theme = createTheme({
    palette: {
      background: {
        default: "#eaeaea",
      },
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Calculator />
    </ThemeProvider>
  );
}

export default App;
