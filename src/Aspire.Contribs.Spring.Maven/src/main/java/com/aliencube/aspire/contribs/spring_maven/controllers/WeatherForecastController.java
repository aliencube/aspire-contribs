package com.aliencube.aspire.contribs.spring_maven.controllers;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class WeatherForecastController {

    @RequestMapping("/")
    String home() {
        return "Hello World!";
    }

}
