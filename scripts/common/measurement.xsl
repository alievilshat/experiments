<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_measurement')">
          <measurement>
            <measurementid m:left="-132" m:top="160" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="id" />
            </measurementid>
            <name m:left="-70" m:top="190" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="name" />
            </name>
            <shortname m:left="-70" m:top="220" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="shortname" />
            </shortname>
            <factor m:left="-70" m:top="252" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="factor" />
            </factor>
            <isdefault m:left="-72" m:top="284" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="isdefault" />
            </isdefault>
            <quantitydecimals m:left="-72" m:top="318" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="quantitydecimals" />
            </quantitydecimals>
            <system m:left="-70" m:top="352" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="system" />
            </system>
          </measurement>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>