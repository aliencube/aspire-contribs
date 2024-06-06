package com.aliencube.aspire.contribs.spring_maven.controllers;

import java.time.LocalDate;
import java.util.HashMap;
import java.util.Map;
import java.util.Random;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

import main.java.com.aliencube.aspire.contribs.spring_maven.models.WeatherForecast;

import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class WeatherForecastController {

    private final String[] summaries = {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

    @GetMapping(value = "/weatherforecast", produces = MediaType.APPLICATION_JSON_VALUE)
    public WeatherForecast[] getWeatherForecast() {
        Random random = new Random();
        return IntStream.range(1, 6)
                .mapToObj(i -> new WeatherForecast(
                        LocalDate.now().plusDays(i),
                        random.nextInt(75) - 20,
                        summaries[random.nextInt(summaries.length)]))
                .toArray(WeatherForecast[]::new);
    }

}
