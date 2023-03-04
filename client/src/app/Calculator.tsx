import { LoadingButton } from "@mui/lab";
import { Alert, Box, TextField } from "@mui/material";
import { Container } from "@mui/system";
import { useState } from "react";
import { useForm } from "react-hook-form";
import agent from "./api/agent";

export default function Calculator() {
  const [result, setResult] = useState(0);
  const [servError, setServError] = useState<{status:number, title:string}>();

  const {
    register,
    handleSubmit,
    setError,
    formState: { isSubmitting, errors, isValid },
  } = useForm({
    mode: "onTouched",
  });

  function handleApiErrors(errors: any) {
    if (errors) {
      setServError(errors.data);
      setError("expression", { message: errors });
      console.log(errors);
    }
  }

  return (
    <>
      {servError?.status !== 500?(
        <Container
          maxWidth="sm"
          sx={{
            display: "flex",
            flexDirection: "column",
            p: 24,
          }}
        >
          <h1 style={{ color: "blue" }}>Calculator</h1>

          <Box
            component="form"
            onSubmit={handleSubmit((data) => {
              if (data.expression.replace(/^\s+/, "")) {
                agent.Calculator.calculate(data)
                  .then((response) => setResult(response.result))
                  .catch((error) => handleApiErrors(error));
              }
            })}
            noValidate
            sx={{ alignItems: "center" }}
          >
            <TextField
              fullWidth
              required={true}
              margin="normal"
              label="Please provide your expression"
              autoFocus
              {...register("expression", {
                required: "Input is required",
                pattern: {
                  value:
                    /(?:(?:((?:(?:[ \t]+))))|(?:((?:(?:\/\/.*?$))))|(?:((?:(?:(?<![\d.])[0-9]+(?![\d.])))))|(?:((?:(?:[0-9]+\.(?:[0-9]+\b)?|\.[0-9]+))))|(?:((?:(?:(?:\+)))))|(?:((?:(?:(?:\-)))))|(?:((?:(?:(?:\*)))))|(?:((?:(?:(?:\/)))))|(?:((?:(?:(?:%)))))|(?:((?:(?:(?:\()))))|(?:((?:(?:(?:\)))))))/,
                  message:
                    "Not a valid expression. Only numbers, parentheses and operators:'+, -, *, /, (, )' allowed",
                },
              })}
              error={!!errors}
              helperText={errors?.expression?.message as string}
            />

            <LoadingButton
              loading={isSubmitting}
              disabled={!isValid}
              type="submit"
              variant="contained"
              sx={{ mt: 3, mb: 3 }}
            >
              Show Result (=)
            </LoadingButton>
          </Box>

          <TextField disabled value={result}></TextField>
        </Container>
      ) : (
        <Alert severity="error">{servError.title}</Alert>
      )}
    </>
  );
}
